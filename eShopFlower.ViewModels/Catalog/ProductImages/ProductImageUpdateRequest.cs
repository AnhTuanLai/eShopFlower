using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace eShopFlower.ViewModels.Catalog.ProductImages
{
    public class ProductImageUpdateRequest
    {
        public string? Caption { get; set; }

        public bool IsDefault { get; set; }

        public int SortOrder { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
