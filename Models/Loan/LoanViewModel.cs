using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PozyczkoPrzypominajkaV2.Models.Loan
{
	public class LoanViewModel
	{
		[Display(AutoGenerateField = false)]
		public int? LoanId { get; set; }

		[Display(Name = "Udzielenie"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:d}")]
		public DateTime Date { get; set; }

		[Display(Name = "Udzielający"), DataType(DataType.Text), Editable(allowEdit: false, AllowInitialValue = true)]
		public string GiverName { get; set; }

		[Display(AutoGenerateField = false)]
		public string GiverId { get; set; }

		[Display(Name = "Biorący"), DataType(DataType.Text)]
		public List<SelectListItem> Receivers { get; set; }

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

		public LoanViewModel(
			int? loanId,
			DateTime date,
			string giverName,
			string giverId,
			List<SelectListItem> receivers,
			decimal amount,
			DateTime repaymentDate,
			decimal repaymentAmount,
			double interest,
			string status
			)
		{
			LoanId = loanId;
			Date = date;
			GiverName = giverName;
			GiverId = giverId;
			Receivers = receivers;
			Amount = amount;
			RepaymentDate = repaymentDate;
			RepaymentAmount = repaymentAmount;
			Interest = interest;
			Status = status;
		}

		public void Init(List<Tuple<string, string>> userList, string? selectedId = null)
		{
			InitReceivers(userList, selectedId);
		}

		private List<SelectListItem> InitReceivers(List<Tuple<string, string>> userList, string? selectedId)
		{
			// userList is expected as Tuple<PrimaryKey.ToString(), AppUser.ToString()>

			var ret = new List<SelectListItem>();

			userList.ForEach(kvp =>
			{
				if (selectedId != null && kvp.Item1 == selectedId)
				{
					ret.Add(new SelectListItem(kvp.Item2, kvp.Item1, selected: true));
				}
				else
				{
					ret.Add(new SelectListItem(kvp.Item2, kvp.Item1, selected: false));
				}
			});

			return ret;
		}
	}
}
