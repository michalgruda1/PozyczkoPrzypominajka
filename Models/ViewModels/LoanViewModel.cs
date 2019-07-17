using PozyczkoPrzypominajka.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PozyczkoPrzypominajkaV2.Models.ViewModels
{
	public class LoanViewModel
	{
		public int LoanID { get; set; }

		[Display(Name = "Udzielenie"), DataType(DataType.Date), DisplayFormat(DataFormatString = "yyyy.MM.dd")]
		public DateTime Date { get; set; }

		[Display(Name = "Udzielający"), DataType(DataType.Text)]
		public string Giver { get; set; }

		[Display(Name = "Biorący"), DataType(DataType.Text)]
		public string Receiver { get; set; }

		[Display(Name = "Pożyczone"), DataType(DataType.Currency), DisplayFormat(DataFormatString = "C0")]
		public decimal Amount { get; set; }

		[Display(Name = "Spłata"), DataType(DataType.Date), DisplayFormat(DataFormatString = "yyyy.MM.dd")]
		public DateTime RepaymentDate { get; set; }

		[Display(Name = "Do oddania"), DataType(DataType.Date), DisplayFormat(DataFormatString = "yyyy.MM.dd")]
		public decimal RepaymentAmount { get; set; }

		[Display(Name = "Oprocentowanie"), DisplayFormat(DataFormatString = "P0")]
		public double Interest { get; set; }

		[DataType(DataType.Text)]
		public string Status { get; set; }
	}
}
