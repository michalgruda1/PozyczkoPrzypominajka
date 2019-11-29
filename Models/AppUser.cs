using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PozyczkoPrzypominajka.Models
{
	public class AppUser : IdentityUser
	{
		[PersonalData]
		[Required, DataType(DataType.Text), Display(Name = "Imię")]
		public string Imie { get; set; } = "Gall";
		[PersonalData]

		[Required, DataType(DataType.Text)]
		public string Nazwisko { get; set; } = "Anonim";

		public override string ToString()
		{
			return Imie + " " + Nazwisko;
		}
	}
}