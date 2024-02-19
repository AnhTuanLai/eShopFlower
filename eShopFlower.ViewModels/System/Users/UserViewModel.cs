﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopFlower.ViewModels.System.Users
{
	public class UserViewModel
	{
		public Guid Id { get; set; }

		[Display(Name = "Tên")]
		public string? FirstName { get; set; }

		[Display(Name = "Họ")]
		public string? LastName { get; set; }

		[Display(Name = "Số điện thoại")]
		public string? PhoneNunber { get; set; }

		[Display(Name = "Tài khoản")]
		public string? UserName { get; set; }

		[Display(Name = "Hòm thư")]
		public string? Email { get; set; }

		[Display(Name = "Ngày sinh")]
		public DateTime Dob { get; set; }

		public IList<string>? Roles { get; set; }
	}
}