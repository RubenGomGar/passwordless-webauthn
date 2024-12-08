using AspNetCorePasswordless.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AspNetCorePasswordless.Areas.Identity.Pages.Account;

[AllowAnonymous]
public class LostAuthenticator : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IEmailSendService _emailSendSender;

    public LostAuthenticator(
        UserManager<IdentityUser> userManager,
        IEmailSendService emailSendSender)
    {
        _userManager = userManager;
        _emailSendSender = emailSendSender;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new InputModel();

    public class InputModel
    {
        [Required]
        public string Email { get; set; }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist or is not confirmed
                return RedirectToPage("./LostAuthenticatorConfirmation");
            }

            await _emailSendSender.ResetPasswordEmail(user);

            return RedirectToPage("./LostAuthenticatorConfirmation");
        }

        return Page();
    }
}
