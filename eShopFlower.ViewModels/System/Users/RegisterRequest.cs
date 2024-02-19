using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopFlower.ViewModels.System.Users
{
    public class RegisterRequest
    {
        [Display(Name = "Tên")]
        public string? FirtName { get; set; }

        [Display(Name = "Họ")]
        public string? LastName { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        [Display(Name = "Hòm thư")]
        public string? Email { get; set; }

        [Display(Name = "Số điện thoại")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Tài khoản")]
        public string? UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Xác nhận tài khoản")]
        [DataType(DataType.Password)]
        public string? ConfirmPass { get; set; }
    }
}