using Models.Dao;
using ShopBanHang.Common;
using ShopBanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ShopBanHang.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            List<CartItem> cartSession = (List<CartItem>)Session[Constants.CART_SESSION];
            return View(cartSession);
        }
        public void AddCart(long productId, int? quantity = 1)
        {
            List<CartItem> cartSession = (List<CartItem>)Session[Constants.CART_SESSION];

            if (cartSession == null)
            {
                CartItem cartItem = new CartItem();
                cartItem.product = new ProductDao().GetProductById(productId);
                cartItem.quantity = quantity.Value;
                List<CartItem> cartItems = new List<CartItem> { cartItem };
                Session[Constants.CART_SESSION] = cartItems;
            }
            else
            {
                if(cartSession.Exists(x=>x.product.ID == productId))
                {
                    cartSession.Where(x => x.product.ID == productId).FirstOrDefault().quantity += quantity.Value;
                }
                else
                {
                    CartItem cartItem = new CartItem();
                    cartItem.product = new ProductDao().GetProductById(productId);
                    cartItem.quantity = quantity.Value;
                    cartSession.Add(cartItem);
                }
                Session[Constants.CART_SESSION] = cartSession;
            }
            Response.Redirect(Request.UrlReferrer.ToString());
            Request.Url.ToString();
        }
        [HttpPost]
        public JsonResult Update(long id, int quantity)
        {
            List<CartItem> cartSession = (List<CartItem>)Session[Constants.CART_SESSION];
            cartSession.Where(x => x.product.ID == id).FirstOrDefault().quantity = quantity;
            Session[Constants.CART_SESSION] = cartSession;
            return Json(new { status = true });
        }
        [HttpPost]
        public JsonResult Delete(string listID)
        {
            List<long> list= new JavaScriptSerializer().Deserialize<List<long>>(listID);
            List<CartItem> cartSession = (List<CartItem>)Session[Constants.CART_SESSION];
            foreach(long id in list)
            {
                cartSession.RemoveAll(x => x.product.ID == id);
            }
            Session[Constants.CART_SESSION] = cartSession;
            return Json(new { status = true });
        }
    }
}