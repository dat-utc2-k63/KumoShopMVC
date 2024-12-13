using System.ComponentModel.DataAnnotations;

namespace KumoShopMVC.ViewModels
{
	public class LoginVM
	{
		[Display(Name = "Email")]
		[Required(ErrorMessage = "*")]
		[EmailAddress(ErrorMessage = "Chưa đúng định dạng Email")]
		public string Email { get; set; }
		[Display(Name = "Password")]
		[Required(ErrorMessage = "*")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

	}
}
