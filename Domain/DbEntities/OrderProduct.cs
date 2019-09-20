namespace Domain
{
    /// <summary>
    /// Таблица продуктов а заказе
    /// </summary>
    public class OrderProduct : DbObject
    {
        public Order Order { get; set; }
        public int OrderId { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }

        public ushort Quantity { get; set; }
    }
}
