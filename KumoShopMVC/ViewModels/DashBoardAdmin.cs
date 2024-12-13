namespace KumoShopMVC.ViewModels
{
    public class DashBoardAdmin
    {
        public int TotalUser {  get; set; }
        public int totalProducts { get; set; }
        public int totalOrders { get; set; }
        public double totalRevenue { get; set; }
        public List<string>? monthYears { get; set; }
        public List<int>? totalOrdersData { get; set; }
        public List<double> totalRevenueData { get; set; }
    }
}
