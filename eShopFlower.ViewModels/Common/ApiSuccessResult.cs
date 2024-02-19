using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopFlower.ViewModels.Common
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T resultObj)
        {
            IsSuccessd = true;
            ResultObject = resultObj;
        }

        public ApiSuccessResult()
        {
            IsSuccessd = true;
        }
    }
}