using System;

namespace PozyczkoPrzypominajka.Models
{
	public class LoanEditModel
	{
		public int? LoanId { get; set; }
		public DateTime DisbursementDate { get; set; }
		public string GiverId { get; set; }
		public string ReceiverId { get; set; }
		public decimal Amount { get; set; }
		public DateTime RepaymentDate { get; set; }
		public decimal RepaymentAmount { get; set; }
		public StatusEnum Status { get; set; }

		public LoanEditModel(
			int? loanId,
			DateTime date,
			string giverId,
			string receiverId,
			decimal amount,
			DateTime repaymentDate,
			decimal repaymentAmount,
			StatusEnum status
			)
		{
			LoanId = loanId;
			DisbursementDate = date;
			GiverId = giverId;
			ReceiverId = receiverId;
			Amount = amount;
			RepaymentDate = repaymentDate;
			RepaymentAmount = repaymentAmount;
			Status = status;
		}
	}
}
