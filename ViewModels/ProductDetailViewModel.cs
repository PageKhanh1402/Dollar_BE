using DollarProject.Models;

namespace DollarProject.ViewModels
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public User Seller { get; set; }
        public List<Product> RelatedProducts { get; set; }
    }

}
