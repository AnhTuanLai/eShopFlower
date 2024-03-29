﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopFlower.ViewModels.Common;
using Microsoft.AspNetCore.Http;

namespace eShopFlower.ViewModels.Catalog.Products
{
    public class ProductUpdateRequest
    {
        public int Id { set; get; }
        public string? Name { set; get; }
        public string? Description { set; get; }
        public string? Details { set; get; }
        public string? SeoDescription { set; get; }
        public string? SeoTitle { set; get; }
        public string? SeoAlias { get; set; }
        public string? LanguageId { set; get; }

        public IFormFile? ThumbnailImage { get; set; }

    }
}
