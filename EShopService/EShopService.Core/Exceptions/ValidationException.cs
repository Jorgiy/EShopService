using System;

namespace EShopService.Core.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }

        public string InvalidFieldName { get; set; }
    }
}