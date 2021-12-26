using BotDetect.Web.Mvc;
using Facebook;
using Models.Dao;
using Models.EF;
using ShopBanHang.Common;
using ShopBanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace ShopBanHang.Controllers
{
    public class UserController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [CaptchaValidationActionFilter("CaptchaCode", "ExampleCaptcha", "Sai xác nhận!")]
        public ActionResult Register(RegisterModel register)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckUserName(register.UserName))
                {
                    MvcCaptcha.ResetCaptcha("ExampleCaptcha");
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại!");
                }
                else if (dao.CheckEmail(register.Email))
                {
                    MvcCaptcha.ResetCaptcha("ExampleCaptcha");
                    ModelState.AddModelError("", "Email đã tồn tại!");
                }
                else
                {
                    var user = new User();
                    user.Name = register.Name;
                    user.Password = register.Password;
                    user.Phone = register.Phone;
                    user.Address = register.Address;
                    user.Email = register.Email;
                    user.UserName = register.UserName;
                    user.CreateDate = DateTime.Now;
                    user.Status = false;
                    long rs = dao.Insert(user);
                    if(rs > 0)
                    {
                        ViewBag.Success = "Đăng ký thành công";
                        register = new RegisterModel();
                    }
                    else
                    {
                        MvcCaptcha.ResetCaptcha("ExampleCaptcha");
                        ModelState.AddModelError("", "Đăng ký không thành công!");
                    }
                }
            }
            return View(register);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserDao dao = new UserDao();
                int rs = dao.Login(model.UserName, model.Password);
                if (rs == 1)
                {
                    User user = dao.GetUser(model.UserName);
                    UserLogin loginSession = new UserLogin();
                    loginSession.UserName = user.UserName;
                    loginSession.UserID = user.ID;
                    Session.Add(Constants.USER_SESSION, loginSession);
                    return Redirect("/");
                }
                else if (rs == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đã bị khóa!");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu sai!");
                }
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            Session[Constants.USER_SESSION] = null;
            return Redirect("/");
        }
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FBAppId"],
                client_secret = ConfigurationManager.AppSettings["FBAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }
    }
}