namespace EShopService.Web.ViewModels
{
    /// <summary>
    /// Model for updating product's descripton
    /// </summary>
    public class UpdateProductDescriptionViewModel
    {
        /// <summary>
        /// Product's identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// New description
        /// </summary>
        public string Description { get; set ; }
    }
}