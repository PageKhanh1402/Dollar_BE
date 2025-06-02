namespace DollarProject.ViewModels
{
    public class SalesStatsViewModel
    {
        public List<string> Labels { get; set; } = new();
        public List<int> Values { get; set; } = new();
        public List<SaleRecord> SaleRecords { get; set; } = new();

        public class SaleRecord
        {
            public string ProductName { get; set; }
            public string BuyerName { get; set; }
            public string SellerName { get; set; }
            public int PriceXu { get; set; }
            public DateTime Date { get; set; }
            public string Status { get; set; }
        }
    }
}
