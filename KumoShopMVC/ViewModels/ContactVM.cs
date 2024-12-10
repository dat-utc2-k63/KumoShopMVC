namespace KumoShopMVC.ViewModels
{
    public class ContactVM
    {
        public int ContactId { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Subject { get; set; }

        public string? DescContact { get; set; }

        public bool? Status { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}