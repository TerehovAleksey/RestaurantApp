using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    /// <summary>
    /// Таблица категория товаров
    /// </summary>
    public class Category : DbObject
    {
        [MaxLength(50), Required]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
