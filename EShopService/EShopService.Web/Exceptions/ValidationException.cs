using System;
using System.Collections.Generic;

namespace EShopService.Web.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }

        public List<string> InvalidFieldNames { get; set; }
    }
}