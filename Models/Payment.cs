using PozyczkoPrzypominajka.Models;
using System;

namespace PozyczkoPrzypominajka.Models
{
	public class Payment
	{
		public int PaymentID { get; set; }
		public int LoanID { get; set; }
		public Loan Loan { get; set; }
		public DateTime PlannedPaymentDate { get; set; }
		public decimal Amount { get; set; }
		public bool IsPaid { get; set; }
	}
}