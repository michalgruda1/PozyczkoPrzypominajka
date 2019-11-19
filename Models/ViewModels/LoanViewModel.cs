using System;
using System.ComponentModel.DataAnnotations;

namespace PozyczkoPrzypominajkaV2.Models.ViewModels
{
	public class LoanViewModel
	{
		public int? LoanID { get; set; }

		[Display(Name = "Udzielenie"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:d}")]
		public DateTime Date { get; set; }

		[Display(AutoGenerateField = false)]
		public Guid GiverId { get; set; }

		[Display(Name = "Udzielający"), DataType(DataType.Text)]
		public string? GiverName { get; set; }

		[Display(AutoGenerateField = false)]
		public Guid ReceiverId { get; set; }

		[Display(Name = "Biorący"), DataType(DataType.Text)]
		public string? ReceiverName { get; set; }

		[Display(Name = "Kwota pożyczki"), DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C2}")]
		public decimal Amount { get; set; }

		[Display(Name = "Spłata"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:d}")]
		public DateTime RepaymentDate { get; set; }

		[Display(Name = "Do oddania"), DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C2}")]
		public decimal RepaymentAmount { get; set; }

		[Display(Name = "Oprocentowanie (RRSO)"), DisplayFormat(DataFormatString = "{0:P0}")]
		public double Interest { get; set; }

		[DataType(DataType.Text)]
		public string Status { get; set; }
	}
}
