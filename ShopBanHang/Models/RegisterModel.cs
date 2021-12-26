using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopBanHang.Models
{
    public class RegisterModel
    {
        public long ID { get; set; }
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Tên đăng nhập bắt buộc nhập!")]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu bắt buộc nhập!")]
        [StringLength(20,MinimumLength =6, ErrorMessage = "Độ dài mật khẩu phải nhiều 6 kí tự và ít hơn 20 kí tự!")]
        public string Password { get; set; }
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage ="Mật khẩu không khớp!")]
        [Required(ErrorMessage = "Nhập lại mật khẩu để so sánh!")]
        public string ComfirmPassword { get; set; }
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Tên bắt buộc nhập!")]
        public string Name { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email bắt buộc nhập!")]
        public string Email { get; set; }
        public string CaptchaCode { get; set; }
    }
}