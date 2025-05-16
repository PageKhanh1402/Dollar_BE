using DollarProject.Models;

namespace DollarProject.Dto
{
    public class ProductDto
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public int PriceXu { get; set; }
        public string? ImageURL { get; set; }
        public string? ProductType { get; set; }
        public string? CategoryName { get; set; }
        public string? SellerName { get; set; }
        public bool IsVerifiedSeller { get; set; }
    }
}
