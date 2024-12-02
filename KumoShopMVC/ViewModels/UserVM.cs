using KumoShopMVC.Data;

namespace KumoShopMVC.ViewModels
{
    public class UserVM
    {
        public int UserId { get; set; }

        public string? Email { get; set; }

        public string? Fullname { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public bool? Status { get; set; }

        public string? Avatar { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? RoleId { get; set; } // Tên Role, được lấy từ đối tượng Role
        public string? Namerole { get; set; }
    }
}
