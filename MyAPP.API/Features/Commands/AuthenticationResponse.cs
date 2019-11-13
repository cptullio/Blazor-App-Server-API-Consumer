using MediatR;

namespace MyAPP.API.Features.Commands
{
    public class AuthenticationResponse 
    {
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}