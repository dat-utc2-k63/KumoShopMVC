using System.ComponentModel.DataAnnotations;

namespace KumoShopMVC.ViewModels
{
    public class UpdateProfileVM
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng Email")]
        public string Email { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "*")]
        [MaxLength(20, ErrorMessage = "Tối đa 20 ký tự")]
        public string FullName { get; set; }

        public string? Avatar { get; set; }

        public bool? Status { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? AboutUs { get; set; }

        [Display(Name = "Current Password")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

    }
}
