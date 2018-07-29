using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectSEM3.EF;

namespace ProjectSEM3.Common
{
    public class AuthorizeController : ActionFilterAttribute
    {
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    var user = HttpContext.Current.Session[CommonConstants.USER_SESSION];

        //    //gia lap da xac dinh duoc danh sach quyen cua nguoi dung
        //    string[] lstPermission = {"Product-Create", "Product-Index"};
        //    //lay ten controller dang dc user goi
        //    string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
        //    //lay ten action cua controller dang duoc goi
        //    string actionName =controllerName +"-"+ filterContext.ActionDescriptor.ActionName;
            
        //    //kiem tra trong []mang co chua hay khong
        //    if (!lstPermission.Contains(actionName))
        //    {
        //        filterContext.Result = new RedirectResult("~/Admin/Home/NotificationAuthorize");
        //    }
        //}


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session[CommonConstants.USER_SESSION] == null)
            {
                filterContext.Result = new RedirectResult("/Admin/Login/Index");
                return;
                
            }

            UserLogin userLogin = (UserLogin) HttpContext.Current.Session[CommonConstants.USER_SESSION];
            var groupId = userLogin.GroupUserID;
            //UserControler-Index
            string actionname = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "Controller-" +
                                filterContext.ActionDescriptor.ActionName;

            //kiểm tra có phải là admin
            SmartShopDbContext db =new SmartShopDbContext();
            var admin = db.Users.SingleOrDefault(x => x.GroupUserID== 2 && x.ID == userLogin.UserID);
            //nếu là admin thì không cần kiểm tra
            if (admin != null)
            {
                return;
            }

            //lấy tên các permission(Usercontroler-index) được gáng cho tài khoản
            var listPermission = from p in db.Permissions
                join g in db.GrantPermissions on p.ID equals g.PermissionID
                where g.GroupUserID == groupId
                select p.PermissionName;
            //nếu tài khoản ko có quyền trong danh sách được cấp thì failed
            if (!listPermission.Contains(actionname))
            {
                filterContext.Result = new RedirectResult("/Admin/Home/NotificationAuthorize");
                return;
            }
        }

       
    }
}