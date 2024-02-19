using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopFlower.Data.Entities;
using eShopFlower.ViewModels.System.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eShopFlower.Application.System.Roles
{
	public class RolesService : IRolesService
	{
		private readonly RoleManager<AppRole> _roleManager;

		public RolesService(RoleManager<AppRole> roleManager)
		{
			_roleManager = roleManager;
		}

		public async Task<List<RoleViewModel>> GetAll()
		{
			var roles = await _roleManager.Roles.Select(x => new RoleViewModel()
			{
				Id = x.Id,
				Name = x.Name,
				Description = x.Description
			}).ToListAsync();

			return roles;
		}
	}
}