using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopFlower.ViewModels.Common
{
    public class ApiResult<T>
    {
        public bool IsSuccessd { get; set; }

        public string? Message { get; set; }

        public T? ResultObject { get; set; }
    }
}