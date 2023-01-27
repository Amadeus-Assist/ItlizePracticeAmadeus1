using DAL_Layer;
using DAL_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Layer
{
    public class BLL_LayerClass
    {
        private string _connStr;
        private DAL_LayerClass _dalLayer;

        public BLL_LayerClass(string connStr)
        {
            _connStr = connStr;
            _dalLayer = new DAL_LayerClass(connStr);
        }

        public bool AddNewUser(UserModel newUser)
        {
            UserModel existUser = _dalLayer.GetUserFromUserName(newUser.UserName);
            if (existUser != null) 
            {
                return false;
            }
            return _dalLayer.AddUser(newUser);
        }

        public List<UserModel> GetAllUsers() 
        {
            return _dalLayer.GetAllUsers();
        }
    }
}
