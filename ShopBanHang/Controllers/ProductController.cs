using Models.Dao;
using Models.EF;
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
            ViewBag.subMenu = new ProductCategoryDao().ListProductCategory();
            ViewBag.parentMenu = new ProductCategoryDao().ListParentProductCategory();

            return PartialView();
        }
        public ActionResult ProductDetail(long proId)
        {
            var model = new ProductDao().GetProductById(proId);
            ViewBag.listRelatedProduct = new ProductDao().ListRalatedProduct(6, model);
            return View(model);
        }
        [ChildActionOnly]
        public ActionResult SlidebarRight()
        {
            var subMenu = new ProductCategoryDao().ListProductCategory();

            return PartialView(subMenu);
        }
        public ActionResult Category(long id, int page = 1, int pageSize = 2)
        {
            long totalRecords = 0;
            List<Product> model;
            var listIDChild = new ProductCategoryDao().ListCategoryIDChild(id);
            if(listIDChild.Count > 0)
            {
                model = new ProductDao().GetListProductByListChildCategoryIdPagination(id, ref totalRecords, page, pageSize, listIDChild);
            }
            else
            {
                model = new ProductDao().GetListProductByCategoryIdPagination(id, ref totalRecords, page, pageSize);
            }
            
            int pagesDisplay = 5;
            int totalPages = (int)Math.Ceiling((float)totalRecords / pageSize);

            ViewBag.page = page;
            ViewBag.productCategory = new ProductCategoryDao().GetProductCategoryByID(id);
            ViewBag.totalPages = totalPages;
            ViewBag.pagesDisplay = pagesDisplay;
            ViewBag.First = 1;
            ViewBag.Last = totalPages;
            ViewBag.Next = page + 1;
            ViewBag.Previous = page - 1;

            return View(model);
        }
    }
}