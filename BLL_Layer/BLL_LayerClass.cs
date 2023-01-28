using DAL_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Layer
{
    public class BLL_LayerClass
    {
        private DAL_LayerClass _dalLayer;

        public BLL_LayerClass()
        {
            _dalLayer = new DAL_LayerClass();
        }

        public bool AddNewUser(USERINFO newUser)
        {
            return _dalLayer.AddUser(newUser);
        }

        public List<USERINFO> GetAllUsers() 
        {
            return _dalLayer.GetAllUsers();
        }
    }
}
