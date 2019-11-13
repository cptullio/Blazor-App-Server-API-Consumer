using MediatR;

namespace MyAPP.API.Features.Commands
{
    public class AuthenticationRequest : IRequest<AuthenticationResponse>
    {
        public string UserName { get; set; }
    }
}