using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    /// <summary>
    /// Таблица заказов
    /// </summary>
    public class Order : DbObject
    {
        [DataType(DataType.DateTime)]
        public DateTime OrderDateTime { get; set; }

        // номер заказа
        public uint Number { get; set; }

        // GUID ID сотрудника
        [MaxLength(450), Required]
        public string ApplicationUserId { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DatePayment { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
        public int PaymentMethodId { get; set; }

        public List<OrderProduct> Products { get; set; }
    }
}
