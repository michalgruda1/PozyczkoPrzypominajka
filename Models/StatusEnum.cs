using System.ComponentModel.DataAnnotations;

namespace PozyczkoPrzypominajka.Models
{
	public enum StatusEnum
	{
		[Display(Name = "Niezdefiniowany")] Undefined = 0,
		[Display(Name = "Nie spłacona")] Unpaid = 1_000,
		[Display(Name = "Spłacona")] Paid = 2_000,
		[Display(Name = "Przeterminowana")] Overdue = 3_000,
	}
}