namespace EShopService.Web.ViewModels
{
    /// <summary>
    /// Model of product
    /// </summary>
    public class ProductViewModel
    {
        /// <summary>
        /// Product's identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Product's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Link to prosuct's image
        /// </summary>
        public string ImgUri { get; set; }
        
        /// <summary>
        /// Product's price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Product's description
        /// </summary>
        public string Description { get; set; }
    }
}