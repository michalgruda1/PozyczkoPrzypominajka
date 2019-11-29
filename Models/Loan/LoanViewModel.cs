using Microsoft.AspNetCore.Mvc.Rendering;
using PozyczkoPrzypominajka.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PozyczkoPrzypominajkaV2.Models.Loan
{
	public class LoanViewModel
	{
		[Display(AutoGenerateField = false)]
		public int? LoanId { get; set; }

		[Display(Name = "Data udzielenia"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:d}")]
		public DateTime DisbursementDate { get; set; }

		[Display(Name = "Udzielający")]
		public IList<SelectListItem> GiverList { get; set; } = new List<SelectListItem>();

		[Display(Name = "Biorący")]
		public IList<SelectListItem> ReceiverList { get; set; } = new List<SelectListItem>();

		[Display(Name = "Kwota pożyczki [PLN]"), DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C2}")]
		public decimal? Amount { get; set; }

		[Display(Name = "Data spłaty"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:d}")]
		public DateTime? RepaymentDate { get; set; }

		[Display(Name = "Kwota spłaty [PLN]"), DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C2}")]
		public decimal? RepaymentAmount { get; set; }

		[Display(Name = "Oprocentowanie (RRSO)"), DataType(DataType.Text), DisplayFormat(DataFormatString = "{0:P0}")]
		public double? Interest { get; set; }

		[EnumDataType(typeof(StatusEnum)), DataType(DataType.Text)]
		public StatusEnum Status { get; set; } = StatusEnum.Undefined;
	}
}
