using DollarProject.Models;

namespace DollarProject.ViewModels
{
    public class ProductFormViewModel
    {
        public Product Product { get; set; } = new Product();
        public List<ProductCategory> Categories { get; set; } = new List<ProductCategory>();
    }

}
