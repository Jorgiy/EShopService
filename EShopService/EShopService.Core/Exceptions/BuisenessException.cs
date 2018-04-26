using System;

namespace EShopService.Core.Exceptions
{
    public class BuisenessException : Exception
    {
        public BuisenessException(string message) : base(message)
        {
        }
    }
}