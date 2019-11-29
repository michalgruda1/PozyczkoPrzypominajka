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

		[Required, Display(Name = "Udzielenie"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:d}")]
		public DateTime? DisbursementDate { get; set; }

		[Required, Display(Name = "Udzielający")]
		public IList<SelectListItem> GiverList { get; set; } = new List<SelectListItem>();

		[Required, Display(Name = "Biorący")]
		public IList<SelectListItem> ReceiverList { get; set; } = new List<SelectListItem>();

		[Required, Display(Name = "Kwota pożyczki"), DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C2}")]
		public decimal? Amount { get; set; }

		[Required, Display(Name = "Spłata"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:d}")]
		public DateTime? RepaymentDate { get; set; }

		[Required, Display(Name = "Do oddania"), DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:C2}")]
		public decimal? RepaymentAmount { get; set; }

		[Required, Display(Name = "Oprocentowanie (RRSO)"), DisplayFormat(DataFormatString = "{0:P0}")]
		public double? Interest { get; set; }

		[Required, DataType(DataType.Text), EnumDataType(typeof(StatusEnum))]
		public StatusEnum Status { get; set; } = StatusEnum.Undefined;
	}
}
