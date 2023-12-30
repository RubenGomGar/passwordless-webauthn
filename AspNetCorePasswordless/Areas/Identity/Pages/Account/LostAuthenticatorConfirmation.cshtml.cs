using AspNetCorePasswordless.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AspNetCorePasswordless.Areas.Identity.Pages.Account;

[AllowAnonymous]
public class LostAuthenticatorConfirmation : PageModel
{

}
