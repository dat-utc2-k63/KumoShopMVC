using System.ComponentModel.DataAnnotations;

namespace KumoShopMVC.ViewModels
{
	public class RegisterVM
	{
		[Display(Name ="Email")]
		[Required(ErrorMessage = "*")]
		[EmailAddress(ErrorMessage ="Chưa đúng định dạng Email")]
		public string Email { get; set; }
		[Display(Name = "Password")]
		[Required(ErrorMessage = "*")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Confirm Password")]
		[Required(ErrorMessage = "*")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
		public string ConfirmPassword { get; set; }
		[Display(Name = "FullName")]
		[Required(ErrorMessage = "*")]
		[MaxLength(20, ErrorMessage = "Tối đa 20 ký tự")]

		public string FullName { get; set; }

		public string? Avatar { get; set; }

		public bool? Status { get; set; }

		public int RoleId { get; set; }

		public DateTime? CreateDate { get; set; }
		public string? Address { get; set; }
		public string? Phone { get; set; }
        public string? AboutUs { get; set; }

    }
}
