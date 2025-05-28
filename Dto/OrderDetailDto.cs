namespace DollarProject.Dto
{
    public class OrderDetailDto
    {
        public int Index { get; set; }
        public int ProductId { get; set; }           // ID của sản phẩm được mua
        public string ProductName { get; set; }
        public int UnitPriceXu { get; set; }
        public int Quantity { get; set; }
        public int TotalPriceXu { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string DeliveryStatus { get; set; }
        public string FullName { get; set; }         // Tên người nhận
        public string Email { get; set; }            // Email người nhận
        public string ShippingAddress { get; set; }  // Địa chỉ giao hàng
        public string PaymentMethod { get; set; }    // Phương thức thanh toán (credit/paypal/...)
    }
}
