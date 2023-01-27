using BLL_Layer;
using DAL_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private BLL_LayerClass _bllLayer;

        public HomeController() {
            string connStr = System.Configuration.ConfigurationManager.AppSettings["NormalTestDBConnectionString"];
            _bllLayer = new BLL_LayerClass(connStr);
        }

        public ActionResult Index(bool redirect=false)
        {
            ViewBag.redirect = redirect;
            return View();
        }
        [HttpPost]
        public ActionResult PostRegister(RegisterData data)
        {
            if (!ModelState.IsValid)
            {

                return RedirectToAction("Index", new {redirect = true});
            }
            bool success = _bllLayer.AddNewUser(new UserModel 
            {
                UserName = data.UserName,
                Password = data.Password,
                Email = data.Email }
            );
            if (!success)
            {
                return RedirectToAction("Index", new { redirect = true });
            }
            return RedirectToAction("Index", new { redirect = false });
        }

        [HttpGet]
        public ActionResult ShowAllUsers() 
        {
            List<UserModel> users = _bllLayer.GetAllUsers();
            return View(users);
        }
    }
}