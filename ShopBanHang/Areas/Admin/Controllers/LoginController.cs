using Models.Dao;
using Models.EF;
using ShopBanHang.Areas.Admin.Models;
using ShopBanHang.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBanHang.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserDao dao = new UserDao();
                var rs = dao.Login(model.UserName, model.Password);
                if (rs)
                {
                    User user = dao.GetUser(model.UserName);
                    UserLogin loginSession = new UserLogin();
                    loginSession.UserName = user.UserName;
                    loginSession.UserID = user.ID;
                    Session.Add(Constants.USER_SESSION, loginSession);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu sai!");
                }
            }
            return View("Index");
        }
    }
}