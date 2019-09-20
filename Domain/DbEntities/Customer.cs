using System.ComponentModel.DataAnnotations;

namespace Domain
{
    /// <summary>
    /// Таблица клиентов
    /// </summary>
    public class Customer : DbObject
    {
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
