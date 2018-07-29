using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectSEM3.EF;

namespace ProjectSEM3.Dao
{
    public class PermissionDao
    {
        private SmartShopDbContext db = null;

        public PermissionDao()
        {
            db = new SmartShopDbContext();
        }

        //lay danh sach theo id
        public IEnumerable<Permission> GetByID(string id)
        {
            IQueryable<Permission> permissions = db.Permissions.Where(x => x.BusinessID == id);
            return permissions;
        }

        //lay doi tuong
        public Permission ViewDetail(long id)
        {
            return db.Permissions.Find(id);
        }

        public bool Edit(Permission permission)
        {
            try
            {
                var model = ViewDetail(permission.ID);
                model.Desciption = permission.Desciption;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        /// <summary>
        /// xóa business
        /// xóa các permission hiện có của business 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            try
            {
                var model = db.Permissions.Find(id);
                db.Permissions.Remove(model);//xóa các action
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }
    }
}