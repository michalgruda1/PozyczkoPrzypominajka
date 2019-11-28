using System;
using System.ComponentModel.DataAnnotations;

namespace PozyczkoPrzypominajka.Models
{
	public class LoanInputModel
	{
		[Required]
		public DateTime DisbursementDate { get; set; }

		[Required]
		public string GiverID { get; set; }

		[Required]
		public string ReceiverID { get; set; }

		[Required]
		[Range(0.01D, 999_999_999.9999D)]
		public decimal Amount { get; set; }

		[Required]
		public DateTime RepaymentDate { get; set; }

		[Required]
		[Range(0.01D, 999_999_999.9999D)]
		public decimal RepaymentAmount { get; set; }

		[Required]
		public StatusEnum Status { get; set; }

		public LoanInputModel(
			DateTime disbursementDate,
			string giverID,
			string receiverID,
			decimal amount,
			DateTime repaymentDate,
			decimal repaymentAmount,
			StatusEnum status)
		{
			DisbursementDate = disbursementDate;
			GiverID = giverID;
			ReceiverID = receiverID;
			Amount = amount;
			RepaymentDate = repaymentDate;
			RepaymentAmount = repaymentAmount;
			Status = status;
		}
	}
}
