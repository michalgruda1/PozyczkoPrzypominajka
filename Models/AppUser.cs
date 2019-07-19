using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PozyczkoPrzypominajka.Models
{
	public class AppUser : IdentityUser
	{
		[PersonalData]
		public string Imie { get; set; }
		[PersonalData]
		public string Nazwisko { get; set; }

		public override string ToString()
		{
			return Imie + " " + Nazwisko;
		}
	}
}