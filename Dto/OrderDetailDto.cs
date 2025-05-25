namespace DollarProject.Dto
{
    public class OrderDetailDto
    {
        public int ProductId { get; set; }           // ID của sản phẩm được mua
        public string FullName { get; set; }         // Tên người nhận
        public string Email { get; set; }            // Email người nhận
        public string ShippingAddress { get; set; }  // Địa chỉ giao hàng
        public string PaymentMethod { get; set; }    // Phương thức thanh toán (credit/paypal/...)
        public int TotalPriceXu { get; set; }        // Tổng giá xu (giá sản phẩm)
    }
}
