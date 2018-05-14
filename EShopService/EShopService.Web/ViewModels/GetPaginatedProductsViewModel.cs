namespace EShopService.Web.ViewModels
{
    /// <summary>
    /// Model of request for pafinated product's data
    /// </summary>
    public class GetPaginatedProductsViewModel
    {
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Page number (if page size is not defined can not be defined)
        /// </summary>
        public int PageNumber { get; set; }
    }
}