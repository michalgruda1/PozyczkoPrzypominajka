using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PozyczkoPrzypominajka.Models
{
	public class Loan
	{
		public int? LoanID { get; set; }

		[Display(Name = "Udzielenie"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:d}")]
		public DateTime Date { get; set; }

		// Zastosowanie prawidłowego typu danych Guid daje błąd:
		// InvalidOperationException: The relationship from 'Loan.Giver' to 'AppUser' with foreign key properties {'GiverID' : Guid} cannot target the primary key {'Id' : string} because it is not compatible. Configure a principal key or a set of compatible foreign key properties for this relationship.
		// którego nie udało się rozwiązać via FluentAPI - HasConversion<string>()
		public string GiverID { get; set; }

		[Display(Name = "Udzielający"), DataType(DataType.Text)]
		public AppUser Giver { get; set; }

		public string ReceiverID { get; set; }

		[Display(Name = "Biorący"), DataType(DataType.Text)]
		public AppUser Receiver { get; set; }

		[Display(Name = "Kwota pożyczki"),
			Range(0.01D, 999_999_999.9999D),
			DataType(DataType.Currency),
			DisplayFormat(DataFormatString = "{0:C2}")]
		public decimal Amount { get; set; }

		[Display(Name = "Spłata"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:d}")]
		public DateTime RepaymentDate { get; set; }

		[Display(Name = "Do oddania"),
			Range(0.01D, 999_999_999.9999D),
			DataType(DataType.Currency),
			DisplayFormat(DataFormatString = "{0:C2}")]
		public decimal RepaymentAmount { get; set; }

		[Display(Name = "Oprocentowanie w skali roku (RRSO)"),
			DisplayFormat(DataFormatString = "{0:P0}",
			ApplyFormatInEditMode = true)]
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
			ICollection<Notification> notifications)
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
