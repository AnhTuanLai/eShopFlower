﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopFlower.ViewModels.Common;

namespace eShopFlower.ViewModels.Catalog.Products
{
    public class GetPublicProductPadingRequest : PagingRequestBase
    {
       public int? CategoryId { get; set; }
    }
}
