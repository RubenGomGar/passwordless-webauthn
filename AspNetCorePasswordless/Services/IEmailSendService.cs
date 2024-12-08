namespace AspNetCorePasswordless.Services
{
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public interface IEmailSendService
    {
        Task ResetPasswordEmail(IdentityUser user);
    }
}
