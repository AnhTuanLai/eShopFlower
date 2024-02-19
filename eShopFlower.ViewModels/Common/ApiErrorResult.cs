using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopFlower.ViewModels.Common
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        public string[] ValidationErrors { get; set; }

        public ApiErrorResult()
        { }

        public ApiErrorResult(string message)
        {
            IsSuccessd = false;
            Message = message;
        }

        public ApiErrorResult(string[] validationErrors)
        {
            IsSuccessd = false;
            ValidationErrors = validationErrors;
        }
    }
}