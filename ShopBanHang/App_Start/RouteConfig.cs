using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopBanHang
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}",
              new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });
            
            routes.MapRoute(
             name: "Đăng nhập",
             url: "dang-nhap",
             defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
             namespaces: new[] { "ShopBanHang.Controllers" }
         );
            routes.MapRoute(
              name: "Đăng kí",
              url: "dang-ky",
              defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
              namespaces: new[] { "ShopBanHang.Controllers" }
          );
            routes.MapRoute(
              name: "Cart",
              url: "gio-hang/",
              defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "ShopBanHang.Controllers" }
          );
            routes.MapRoute(
              name: "Add to cart",
              url: "them-gio-hang/",
              defaults: new { controller = "Cart", action = "AddCart", id = UrlParameter.Optional },
              namespaces: new[] { "ShopBanHang.Controllers" }
          );
            routes.MapRoute(
               name: "Product Category",
               url: "san-pham/{productCategory}-{id}",
               defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
               namespaces: new[] { "ShopBanHang.Controllers" }
           );

            routes.MapRoute(
                name: "Product Detail",
                url: "chi-tiet/{metatitle}-{proId}",
                defaults: new { controller = "Product", action = "ProductDetail", },
                namespaces: new[] { "ShopBanHang.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ShopBanHang.Controllers" }
            );
        }
    }
}
