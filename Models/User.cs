﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PozyczkoPrzypominajka.Models
{
	public class User : IdentityUser
	{
		public int UserID { get; set; }
		[PersonalData]
		public string Imie { get; set; }
		[PersonalData]
		public string Nazwisko { get; set; }
	}
}