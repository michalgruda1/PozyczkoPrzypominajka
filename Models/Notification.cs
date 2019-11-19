using System;

namespace PozyczkoPrzypominajka.Models
{
	public class Notification
	{
		public int? NotificationID { get; set; }
		public int LoanID {get; set;}
		public Loan Loan { get; set; }
		public DateTime When { get; set; }
		public string Message { get; set; }
		public ChannelEnum Channel { get; set; }

		public Notification(int notificationID, int loanID, Loan loan, DateTime when, string message, ChannelEnum channel)
		{
			NotificationID = notificationID;
			LoanID = loanID;
			Loan = loan;
			When = when;
			Message = message;
			Channel = channel;
		}
	}
}