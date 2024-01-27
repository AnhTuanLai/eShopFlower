using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopFlower.Utilities.Exceptions
{
    public class eShopFlowerException : Exception
    {
        public eShopFlowerException() { }

        public eShopFlowerException(string message) : base(message) { }

        public eShopFlowerException(string message, Exception inner) : base(message,inner) { }
    }
}
