using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace eShopFlower.Data.EF
{
    public class eShopFlowerDbContextFactory : IDesignTimeDbContextFactory<eShopFlowerDbContext>
    {
        public eShopFlowerDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = configuration.GetConnectionString("ShopFlowerDB");

            var optionBuilder = new DbContextOptionsBuilder<eShopFlowerDbContext>();
            optionBuilder.UseSqlServer(connectionString);

            return new eShopFlowerDbContext(optionBuilder.Options);
        }
    }
}
