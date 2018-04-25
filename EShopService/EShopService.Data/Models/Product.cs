using System.ComponentModel.DataAnnotations.Schema;

namespace EShopService.Data.Models
{
    [Table("PRODUCTS")]
    public class Product
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("IMG_URI")]
        public string ImgUri { get; set; }
        
        [Column("PRICE")]
        public decimal Price { get; set; }

        [Column("DESCRIPTION")]
        public string Description { get; set; }
    }
}