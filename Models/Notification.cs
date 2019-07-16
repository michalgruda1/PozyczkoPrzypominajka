using System;

namespace PozyczkoPrzypominajka.Models
{
	public class Notification
	{
		public int NotificationID { get; set; }
		public int LoanID {get; set;}
		public Loan Loan { get; set; }
		public DateTime When { get; set; }
		public string Text { get; set; }
		public ChannelEnum Channel { get; set; }
	}
}