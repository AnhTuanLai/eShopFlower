using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopFlower.Data.Entities;
using eShopFlower.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eShopFlower.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
              new AppConfig() { Key = "HomeTitle", Value = "This is home page of eShopFlower" },
              new AppConfig() { Key = "HomeKeyword", Value = "This is keyword of eShopFlower" },
              new AppConfig() { Key = "HomeDescription", Value = "This is description of eShopFlower" }
              );
            modelBuilder.Entity<Language>().HasData(
                new Language() { Id = "vi", Name = "Tiếng Việt", IsDefault = true },
                new Language() { Id = "en", Name = "English", IsDefault = false });

            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 1,
                    Status = Status.Active,
                },
                 new Category()
                 {
                     Id = 2,
                     IsShowOnHome = true,
                     ParentId = null,
                     SortOrder = 2,
                     Status = Status.Active
                 });

            modelBuilder.Entity<CategoryTranslation>().HasData(
                  new CategoryTranslation() { Id = 1, CategoryId = 1, Name = "Hoa Sinh Nhật", LanguageId = "vi", SeoAlias = "hoa-sinh-nhat", SeoDescription = "Sản phẩm hoa sinh nhật", SeoTitle = "Sản phẩm hoa sinh nhật" },
                  new CategoryTranslation() { Id = 2, CategoryId = 1, Name = "Birthday Flowers", LanguageId = "en", SeoAlias = "birthday-flowers", SeoDescription = "The birthday flower products ", SeoTitle = "The birthday flower products " },
                  new CategoryTranslation() { Id = 3, CategoryId = 2, Name = "Hoa Khai Trương", LanguageId = "vi", SeoAlias = "hoa-khai-truong", SeoDescription = "Sản phẩm hoa khai trương", SeoTitle = "Sản phẩm hoa khai trương" },
                  new CategoryTranslation() { Id = 4, CategoryId = 2, Name = "Grand Opening Flower", LanguageId = "en", SeoAlias = "grand-opening-flower", SeoDescription = "The grand opening flower products", SeoTitle = "The grand opening flower products" }
                    );

            modelBuilder.Entity<Product>().HasData(
           new Product()
           {
               Id = 1,
               DateCreated = DateTime.Now,
               OriginalPrice = 100000,
               Price = 200000,
               Stock = 0,
               ViewCount = 0,
           },
            new Product()
            {
                Id = 2,
                DateCreated = DateTime.Now,
                OriginalPrice = 100000,
                Price = 200000,
                Stock = 0,
                ViewCount = 0,
            }
           );
           

            modelBuilder.Entity<ProductTranslation>().HasData(
                 new ProductTranslation()
                 {
                     Id = 1,
                     ProductId = 1,
                     Name = "Bó hoa tươi - Nàng tiên",
                     LanguageId = "vi",
                     SeoAlias = "bó-hoa-tuoi-nang-tien",
                     SeoDescription = "Bó hoa tươi - Nàng tiên",
                     SeoTitle = "Bó hoa tươi - Nàng tiên",
                     Details = "Bó hoa tươi - Nàng tiên",
                     Description = "Bó hoa tươi - Nàng tiên"
                 },
                    new ProductTranslation()
                    {
                        Id = 2,
                        ProductId = 1,
                        Name = "Fresh flower bouquet - Fairy",
                        LanguageId = "en",
                        SeoAlias = "fresh-flower-bouquet-fairy",
                        SeoDescription = "Fresh flower bouquet - Fairy",
                        SeoTitle = "Fresh flower bouquet - Fairy",
                        Details = "Fresh flower bouquet - Fairy",
                        Description = "Fresh flower bouquet - Fairy"
                    },
                   new ProductTranslation()
                   {
                       Id = 3,
                       ProductId = 2,
                       Name = "Bó hoa tươi - Tinh tú",
                       LanguageId = "vi",
                       SeoAlias = "bó-hoa-tuoi-tinh-tu",
                       SeoDescription = "Bó hoa tươi - Tinh tú",
                       SeoTitle = "Bó hoa tươi - Tinh tú",
                       Details = "Bó hoa tươi - Tinh tú",
                       Description = "Bó hoa tươi - Tinh tú"
                   },
                   new ProductTranslation()
                   {
                       Id = 4,
                       ProductId = 2,
                       Name = "Fresh flower bouquet - Fairy",
                       LanguageId = "en",
                       SeoAlias = "fresh-flower-bouquet-stars",
                       SeoDescription = "Fresh flower bouquet - Stars",
                       SeoTitle = "Fresh flower bouquet - Stars",
                       Details = "Fresh flower bouquet - Stars",
                       Description = "Fresh flower bouquet - Stars"
                   }

                    );

            modelBuilder.Entity<ProductInCategory>().HasData(
                new ProductInCategory() { ProductId = 1, CategoryId = 1 },
             new ProductInCategory() { ProductId = 2, CategoryId = 2 }
                );
            // any guid
            var roleId = new Guid("F8F0B9B6-4F9B-4AF6-B02C-15096528DC89");
            var adminId = new Guid("DC5FBC3B-E067-4D0C-80E3-00348A067D79");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser?>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "tuananhlai0920@gmail.com",
                NormalizedEmail = "tuananhlai0920@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abcd1234$"),
                SecurityStamp = string.Empty,
                FirstName = "Hung",
                LastName = "Nguyen",
                Dob = new DateTime(2020, 01, 31)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }

    }
}
