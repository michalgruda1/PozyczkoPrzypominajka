using System;
using System.ComponentModel.DataAnnotations;

namespace PozyczkoPrzypominajka.Models
{
	public class LoanEditModel
	{
		public int? LoanId { get; set; }

		[Display(Name = "Data udzielenia")]
		[Required(ErrorMessage = "{0} jest wymagana")]
		[DataType(DataType.Date)]
		public DateTime DisbursementDate { get; set; }

		[Display(Name = "Udzielający")]
		[Required(ErrorMessage = "{0} jest wymagany")]
		[DataType(DataType.Text)]
		public string GiverId { get; set; }

		[Display(Name = "Biorący")]
		[Required(ErrorMessage = "{0} jest wymagany")]
		[DataType(DataType.Text)]
		public string ReceiverId { get; set; }

		[Display(Name = "Kwota pożyczki [PLN]")]
		[Required(ErrorMessage = "{0} jest wymagana")]
		[DataType(DataType.Currency)]
		public decimal Amount { get; set; }

		[Display(Name = "Data spłaty")]
		[Required(ErrorMessage = "{0} jest wymagana")]
		[DataType(DataType.Date)]
		public DateTime RepaymentDate { get; set; }

		[Display(Name = "Kwota spłaty [PLN]")]
		[Required(ErrorMessage = "{0} jest wymagana")]
		[DataType(DataType.Currency)]
		public decimal RepaymentAmount { get; set; }

		[Required(ErrorMessage = "{0} jest wymagany")]
		[EnumDataType(typeof(StatusEnum)), DataType(DataType.Text)]
		public StatusEnum Status { get; set; }

		public LoanEditModel()
		{
		}

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
