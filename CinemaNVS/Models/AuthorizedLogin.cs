using CinemasNVS.BLL.DTOs;

namespace CinemaNVS.Models
{
    public class AuthorizedLogin
    {
        public LoginResponse loginResponse { get; }
        public string JWToken { get; }

        public AuthorizedLogin(LoginResponse loginResponse, string jWToken)
        {
            this.loginResponse = loginResponse;
            JWToken = jWToken;
        }
    }
}
