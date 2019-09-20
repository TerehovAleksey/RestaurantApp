using System.ComponentModel.DataAnnotations;

namespace Domain
{
    /// <summary>
    /// Таблица способов оплаты
    /// </summary>
    public class PaymentMethod : DbObject
    {
        [MaxLength(50)]
        public string PaymentName { get; set; }
    }
}
