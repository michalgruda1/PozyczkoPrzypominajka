using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace PozyczkoPrzypominajka.Models
{
	public class Loan
	{
		public int LoanID { get; set; }
		public DateTime Date { get; set; }
		public AppUser Giver { get; set; }
		public AppUser Receiver { get; set; }
		public decimal Amount { get; set; }
		public DateTime RepaymentDate { get; set; }
		public decimal RepaymentAmount { get; set; }
		public double Interest
		{
			get
			{
				TimeSpan LoanPeriod = RepaymentDate - Date;
				if (LoanPeriod == TimeSpan.Zero || Amount == 0) return 0;
				return Math.Pow(Decimal.ToDouble(RepaymentAmount / Amount), 365D / (RepaymentDate - Date).TotalDays);
			}
		}
		public ICollection<Payment> Payments { get; set; }
		public ICollection<Notification> Notifications { get; set; }
	}
}
