using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopFlower.ViewModels.Common;

namespace eShopFlower.ViewModels.Catalog.Products
{
    public class GetManageProductPadingRequest : PagingRequestBase
    {
        public string? Keyword { get; set; }

        public List<int>? CategoryIds { get; set; }
    }
}
