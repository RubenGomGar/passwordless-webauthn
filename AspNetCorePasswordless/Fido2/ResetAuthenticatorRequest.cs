using Fido2NetLib;

namespace AspNetCorePasswordless.Fido2
{
    public class ResetAuthenticatorRequest
    {
        public string Code { get; set; }
        public string Email { get; set; }
        public AuthenticatorAttestationRawResponse AttestationResponse {  get; set; }
    }
}
