using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PozyczkoPrzypominajka.Models;

namespace PozyczkoPrzypominajkaV2.Areas.Identity.Pages.Account.Manage
{
	public partial class IndexModel : PageModel
	{
		private readonly UserManager<AppUser> userManager;
		private readonly SignInManager<AppUser> signInManager;
		private readonly IEmailSender emailSender;

		public IndexModel(
				UserManager<AppUser> userManager,
				SignInManager<AppUser> signInManager,
				IEmailSender emailSender)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.emailSender = emailSender;
		}

		public string Username { get; set; }

		public bool IsEmailConfirmed { get; set; }

		[TempData]
		public string StatusMessage { get; set; }

		[BindProperty]
		public InputModel Input { get; set; }

		public class InputModel
		{
			[Required]
			[DataType(DataType.Text)]
			[Display(Name = "Imię")]
			public string Imie { get; set; }

			[Required]
			[DataType(DataType.Text)]
			public string Nazwisko { get; set; }

			[Required]
			[EmailAddress]
			public string Email { get; set; }

			[Phone]
			[Display(Name = "Phone number")]
			public string PhoneNumber { get; set; }
		}

		public async Task<IActionResult> OnGetAsync()
		{
			var user = await userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
			}

			var userName = await userManager.GetUserNameAsync(user);
			var email = await userManager.GetEmailAsync(user);
			var phoneNumber = await userManager.GetPhoneNumberAsync(user);
			var imie = user.Imie;
			var nazwisko = user.Nazwisko;

			Username = userName;

			Input = new InputModel
			{
				Email = email,
				PhoneNumber = phoneNumber,
				Imie = imie,
				Nazwisko = nazwisko,
			};

			IsEmailConfirmed = await userManager.IsEmailConfirmedAsync(user);

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var user = await userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
			}

			var email = await userManager.GetEmailAsync(user);
			if (Input.Email != email)
			{
				var setEmailResult = await userManager.SetEmailAsync(user, Input.Email);
				if (!setEmailResult.Succeeded)
				{
					var userId = await userManager.GetUserIdAsync(user);
					throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
				}
			}

			var phoneNumber = await userManager.GetPhoneNumberAsync(user);
			if (Input.PhoneNumber != phoneNumber)
			{
				var setPhoneResult = await userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
				if (!setPhoneResult.Succeeded)
				{
					var userId = await userManager.GetUserIdAsync(user);
					throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
				}
			}

			if (user.Imie != Input.Imie)
			{
				user.Imie = Input.Imie;
			}

			if (user.Nazwisko != Input.Nazwisko)
			{
				user.Nazwisko = Input.Nazwisko;
			}

			await userManager.UpdateAsync(user);

			await signInManager.RefreshSignInAsync(user);
			StatusMessage = "Your profile has been updated";
			return RedirectToPage();
		}

		public async Task<IActionResult> OnPostSendVerificationEmailAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var user = await userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
			}


			var userId = await userManager.GetUserIdAsync(user);
			var email = await userManager.GetEmailAsync(user);
			var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
			var callbackUrl = Url.Page(
					"/Account/ConfirmEmail",
					pageHandler: null,
					values: new { userId = userId, code = code },
					protocol: Request.Scheme);
			await emailSender.SendEmailAsync(
					email,
					"Confirm your email",
					$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

			StatusMessage = "Verification email sent. Please check your email.";
			return RedirectToPage();
		}
	}
}
