using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ProjectSEM3.Common;
using ProjectSEM3.Dao;
using ProjectSEM3.EF;

namespace ProjectSEM3.Areas.Admin.Controllers
{
    [AuthorizeController]
    public class BusinessController : Controller
    {
        // GET: Admin/Business
        BusinessDao db = new BusinessDao();
        public ActionResult Index()
        {
            var model = db.GetAllBusinesses();
            return View(model);
        }

        //update lại tất cả các conroller và action
        public ActionResult Update()
        {
            var model = db.UpdateBusiess("ProjectSEM3.Areas.Admin.Controllers");
            if (model)
            {
                TempData["Success"] = "Thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Thất bại";
                return RedirectToAction("Index");
            }
        }
    
        //cập nhập 
        [HttpPost]
        public JsonResult Edit(string data)
        {

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            var model = javaScriptSerializer.Deserialize<Business>(data);
            bool status = db.Edit(model);
            return Json(new
            {
                status = status
            });
        }

        [HttpPost]
        public JsonResult Delete(string id)
        {
            var business = new BusinessDao();
            var model = business.Delete(id);
            return Json(new
            {
                status = model
            });
        }

        [HttpPost]
        public JsonResult ViewDetail(string id)
        {

            var model = db.ViewDetail(id);
            return Json(new
            {
                status = true,
                id = model.ID,
                name = model.Name
            }, JsonRequestBehavior.AllowGet);
        }
    }
}