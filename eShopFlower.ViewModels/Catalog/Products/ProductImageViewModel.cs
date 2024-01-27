using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopFlower.ViewModels.Catalog.Products
{
    public class ProductImageViewModel
    {
        public int Id { get; set; }

        public string FilePath { get; set; } = string.Empty;

        public bool IsDefault { get; set; }

        public long FileSize { get; set; }
    }
}
