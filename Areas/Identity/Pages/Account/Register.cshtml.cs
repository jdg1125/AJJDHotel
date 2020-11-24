using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using AJJDHotel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using AJJDHotel.Data;
using AJJDHotel.Utility;

using System.Net;
using System.Net.Mail;

namespace AJJDHotel.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _db = db;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Birthday")]
            [DataType(DataType.Date)]
            public DateTime DOB { get; set; } 

            [Required]
            [Display(Name ="First name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
 

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            //checked the dob code code1
            DateTime now = DateTime.Now;
            int age = new DateTime(DateTime.Now.Subtract(Input.DOB).Ticks).Year - 1;
            if (age < 18) return Page();
             

            //check over

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { 
                    UserName = Input.Email, 
                    Email = Input.Email, 
                    DOB= Input.DOB, 
                    FirstName = Input.FirstName, 
                    LastName = Input.LastName
                };
                AJJDEmailRegister();
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(SD.AdminUser))
                        await _roleManager.CreateAsync(new IdentityRole(SD.AdminUser));

                    if (!await _roleManager.RoleExistsAsync(SD.CustomerUser))
                        await _roleManager.CreateAsync(new IdentityRole(SD.CustomerUser));

                    await _userManager.AddToRoleAsync(user, SD.CustomerUser);

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public void AJJDEmailRegister()
        {
            MailAddress to = new MailAddress(Input.Email);
            MailAddress from = new MailAddress("jtn2dsng@gmail.com");

            MailMessage message = new MailMessage(from, to);
            message.Subject = "AJJD Hotel Registration";
            message.Body = "Thank you for registering at AJJD Hotel";

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("jtn2dsng", "Tw!$3r@1"),
                EnableSsl = true
            };

            try
            {
                client.Send(message);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
