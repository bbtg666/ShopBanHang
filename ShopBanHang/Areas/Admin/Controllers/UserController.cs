using Models.Dao;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using ShopBanHang.Common;

namespace ShopBanHang.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            ViewBag.searchString = searchString;
            var dao = new UserDao();
            var model = dao.padgination(searchString, page, pageSize);
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserDao dao = new UserDao();
                    long id = dao.Insert(user);
                    if (id > 0)
                    {
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm người dùng thất bại!");
                    }
                }
                return View();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }

        }
        public ActionResult Edit(long id)
        {
            var user = new UserDao().GetUserByID(id);
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserLogin modifiedBy = (UserLogin)Session[Constants.USER_SESSION];
                    user.ModifiedBy = modifiedBy.UserName;
                    UserDao dao = new UserDao();
                    bool check = dao.Update(user);
                    if (check)
                    {
                        SetAlert("Cập nhật thành công", "success");
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Cập nhật người dùng thất bại!");
                    }
                }
                return View();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }

        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            UserDao dao = new UserDao();
            dao.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var dao = new UserDao();
            var rs = dao.ChangeStatus(id);
            return Json(new { status = rs}, JsonRequestBehavior.AllowGet);
        }
    }
}