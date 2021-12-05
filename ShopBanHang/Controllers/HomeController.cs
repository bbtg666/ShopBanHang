using Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBanHang.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var listSlide = new SlideDao().ListSlide();
            var dao = new ProductDao();
            ViewBag.listNewProduct = dao.ListNewProduct(8);
            ViewBag.listFeatureProduct = dao.ListFeatureProduct(8);
            return View(listSlide);
        }
        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var model = new MenuDao().ListByGroupID(1);
            return PartialView(model);
        }
        
    }
}