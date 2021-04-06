using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IServices;
using Domain;
using ViewModel;
using AutoMapper;
using AutoMapper.Configuration;
using Common;

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
        public ActionResult Editor()
        {
            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Editor(string con)
        {
            return Json(new { con = con }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase imgFile)
        {
            imgFile.SaveAs(Server.MapPath("/1.jpg"));
            return Json(new { url= "/1.jpg" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(AdminModel adminModel)
        {
            adminServices.Create(adminModel.MapTo<Admin>());
            return Json(new { msg = "添加成功" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult List()
        {
            return View(adminServices.GetListByPage(m => m.AdminID, 1, 10,m => m.AdminID == 1, o => o.UserName.Contains("test")));
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult XM()
        {
            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Query(string UserName, DateTime? dateTime)
        {
            using (erp_1807Entities db = new erp_1807Entities())
            {
                //用户名，末次登录时间
                //string sql = "SELECT * FROM Admin ";

                var list = db.Admin.AsQueryable();

                if(!string.IsNullOrWhiteSpace(UserName))
                {
                    list = list.Where(m => m.UserName.Contains(UserName));
                }

                if(dateTime != null)
                {
                    list = list.Where(m => m.LastLoginTime > dateTime);
                }

                var adminList = list.OrderBy(m => m.AdminID).Skip(10).Take(5).ToList();                
            }


            return null;
        }
    }
}