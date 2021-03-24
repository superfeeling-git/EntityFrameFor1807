using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IServices;
using Domain;

namespace WebUI.Controllers
{
    public class DefaultController : Controller
    {
        private IAdminServices adminServices;

        public DefaultController(IAdminServices _adminServices)
        {
            this.adminServices = _adminServices;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            adminServices.Create(new Admin { UserName = "1807", LastLoginTime = DateTime.Now, Password = Guid.NewGuid().ToString() });
            return null;
        }

        // GET: Default
        public ActionResult Index()
        {
            foreach (var item in adminServices.GetAll())
            {
                Response.Write(item.UserName);
            }
            return new EmptyResult();
        }
    }
}