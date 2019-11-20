using System.ComponentModel.DataAnnotations;

namespace PozyczkoPrzypominajkaV2.Areas.Identity.Pages.Account
{
	public class RegisterInputVM
	{
		[Required]
		[Display(Name = "Imię")]
		public string Imie { get; set; }

		[Required]
		public string Nazwisko { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[StringLength(18, ErrorMessage = "{0} musi mieć przynajmniej {2} znaków i maksymalnie {1} znaków.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Hasło")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Potwierdzenie hasła")]
		[Compare("Password", ErrorMessage = "Hasło oraz Potwierdzenie hasła są inne.")]
		public string ConfirmPassword { get; set; }
	}
}
