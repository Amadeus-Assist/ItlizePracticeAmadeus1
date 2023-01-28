using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Layer
{
    public class DAL_LayerClass
    {
        public bool AddUser(USERINFO newUser)
        {
            using (var dbContext = new NormalTestDBEntities())
            {
                dbContext.USERINFO.Add(newUser);
                try
                {
                    dbContext.SaveChanges();
                } 
                catch(DbUpdateException ex)
                {
                    return false;
                }
                return true;
            }
        }

        public List<USERINFO> GetAllUsers ()
        {
            return new NormalTestDBEntities().USERINFO.ToList();
        }
    }
}
