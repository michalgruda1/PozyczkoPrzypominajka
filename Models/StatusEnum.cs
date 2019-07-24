using System.ComponentModel.DataAnnotations;

namespace PozyczkoPrzypominajka.Models
{
	public enum StatusEnum
	{
		[Display(Name = "Nie spłacona")] Unpaid = 0,
		[Display(Name = "Spłacona")] Paid = 1_000,
		[Display(Name = "Przeterminowana")] Overdue = 2_000,
	}
}