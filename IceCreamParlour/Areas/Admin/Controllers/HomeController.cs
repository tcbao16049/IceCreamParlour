using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using ProjectSEM3.EF;
using ProjectSEM3.DAO;
using ProjectSEM3.Common;
using ProjectSEM3.Dao;
using ProjectSEM3.Models;

namespace ProjectSEM3.Areas.Admin.Controllers
{
   
    public class HomeController : Controller
    {
        // GET: Admin/Home
 
        public ActionResult Index()
        {
            SmartShopDbContext db = new SmartShopDbContext();
            ViewBag.CountUser = db.Users.Count();
            ViewBag.CountProduct = db.Products.Count();
            ViewBag.CountOrder = db.Orders.Count();
            ViewBag.CountContent = db.Contents.Count();
            ViewBag.CountUserAdmin = db.Users.Count(x => x.GroupUserID==2);
            return View();
        }

        public ActionResult NotificationAuthorize()
        {
            return View();
        }

        //private ActionResult RedirectToLocal(string returnUrl)
        //{
        //    if (Url.IsLocalUrl(returnUrl))
        //    {
        //        return Redirect(returnUrl);
        //    }
        //    return RedirectToAction("Index", "Home");
        //}

        //public JsonResult GetChartOrder()
        //{
        //    OrderDAO orderDao = new OrderDAO();
        //    List<string>  list = new List<string>();
        //    foreach (var item in orderDao.GetAllOrder("").OrderByDescending(x=>x.ShipCreateDate))
        //    {
        //        int count = orderDao.ListOrdersDay(item.ShipCreateDate).Count();
        //        string = 
        //        list.Add();
        //    }
        //}

       
    }


}