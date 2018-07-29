using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectSEM3.EF;

namespace ProjectSEM3.Dao
{
   
    public class SlideTypeDAO
    {
        SmartShopDbContext db =null;

        public SlideTypeDAO()
        {
            db = new SmartShopDbContext();
        }

        public IEnumerable<SlideType> GetAll()
        {
            IQueryable<SlideType> list = db.SlideTypes.OrderByDescending(x => x.ID);
            return list;
        }

    }
}