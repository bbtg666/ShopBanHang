using Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBanHang.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
           
            return View();
        }
        [ChildActionOnly]
        public ActionResult ProductCategory()
        {
            var mainMenu = new MenuDao().ListByGroupID(2);
            var subMenu = new ProductCategoryDao().ListProductCategory();
            ViewBag.subMenu = subMenu;

            return PartialView(mainMenu);
        }
        public ActionResult ProductDetail(long proId)
        {
            var model = new ProductDao().GetProductById(proId);
            ViewBag.listRelatedProduct = new ProductDao().ListRalatedProduct(6, model);
            return View(model);
        }
        [ChildActionOnly]
        public ActionResult ProductCategoryDetail()
        {
            var subMenu = new ProductCategoryDao().ListProductCategory();

            return PartialView(subMenu);
        }
    }
}