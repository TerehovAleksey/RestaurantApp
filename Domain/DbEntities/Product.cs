using System.ComponentModel.DataAnnotations;

namespace Domain
{
    /// <summary>
    /// Таблица продуктов (товаров)
    /// </summary>
    public class Product : DbObject
    {
        [MaxLength(50), Required]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
