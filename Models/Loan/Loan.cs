using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PozyczkoPrzypominajka.Models
{
	public class Loan
	{
		public int? LoanID { get; set; }

		public DateTime Date { get; set; }

		// Zastosowanie prawidłowego typu danych Guid daje błąd:
		// InvalidOperationException: The relationship from 'Loan.Giver' to 'AppUser' with foreign key properties {'GiverID' : Guid} cannot target the primary key {'Id' : string} because it is not compatible. Configure a principal key or a set of compatible foreign key properties for this relationship.
		// którego nie udało się rozwiązać via FluentAPI - HasConversion<string>()
		public string GiverID { get; set; }

		public AppUser Giver { get; set; }

		public string ReceiverID { get; set; }

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
				var value = Decimal.ToDouble(RepaymentAmount / Amount);
				var power = 365D / (RepaymentDate - Date).TotalDays;
				var rrso = (Math.Pow(value, power) - 1);
				return rrso;
			}
		}

		public StatusEnum Status { get; set; }

		public ICollection<Notification>? Notifications { get; set; }

		// for ORM use only
		private Loan() { }

		public Loan(
			int? loanID,
			DateTime date,
			string giverID,
			AppUser giver,
			string receiverID, 
			AppUser receiver,
			decimal amount, 
			DateTime repaymentDate, 
			decimal repaymentAmount,
			StatusEnum status,
			ICollection<Notification>? notifications)
		{
			LoanID = loanID;
			Date = date;
			GiverID = giverID;
			Giver = giver;
			ReceiverID = receiverID;
			Receiver = receiver;
			Amount = amount;
			RepaymentDate = repaymentDate;
			RepaymentAmount = repaymentAmount;
			Status = status;
			Notifications = notifications;
		}
	}
}
