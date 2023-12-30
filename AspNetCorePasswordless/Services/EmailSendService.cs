using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace AspNetCorePasswordless.Services
{
    public class EmailSendService : IEmailSendService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<IEmailSendService> _logger;

        public EmailSendService(
            UserManager<IdentityUser> userManager,
            IEmailSender emailSender,
            ILogger<IEmailSendService> logger)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        public async Task ResetPasswordEmail(IdentityUser user)
        {
            // For more information on how to enable account confirmation and password reset please 
            // visit https://go.microsoft.com/fwlink/?LinkID=532713
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var confirmEmailCallback = new Uri("https://localhost:5001/Identity/Account/ResetAuthenticator?code=" + code);

            try
            {
                await _emailSender.SendEmailAsync(
                    user.Email,
                    "Reset password",
                    string.Format("Click the following link to reset password: <a href='{0}'>{1}</a>", confirmEmailCallback, confirmEmailCallback));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to send ResetPassword email to userId: {userId}", user.Id);
                throw;
            }
        }
    }
}
