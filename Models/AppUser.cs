using Microsoft.AspNetCore.Identity;

namespace PozyczkoPrzypominajka.Models
{
	public class AppUser : IdentityUser
	{
		[PersonalData]
		public string Imie { get; set; } = "Gall";
		[PersonalData]
		public string Nazwisko { get; set; } = "Anonim";

		public override string ToString()
		{
			return Imie + " " + Nazwisko;
		}
	}
}