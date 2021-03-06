namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public long ID { get; set; }

        [Required(ErrorMessage = "Tên người dùng không được bỏ trống!")]
        [StringLength(50)]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }
        
        [StringLength(100)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        
        [StringLength(50)]
        [Display(Name = "Họ và tên")]
        public string Name { get; set; }

        [StringLength(50)]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(250)]
        public string CreateBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(250)]
        public string ModifiedBy { get; set; }

        [Display(Name = "Trạng thái hoạt động")]
        public bool Status { get; set; }
    }
}
