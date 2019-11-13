using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyAPP.API.Services;

namespace MyAPP.API.Features.Commands
{
    public class AuthenticationHandler : IRequestHandler<AuthenticationRequest, AuthenticationResponse>
    {
        private ITokenService tokenService;

        public AuthenticationHandler()
        {
        }

        public AuthenticationHandler(ITokenService _tokenService)
        {
            tokenService = _tokenService;
        }

        public Task<AuthenticationResponse> Handle(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var role = UserService.GetRoleByUserName(request.UserName);
                if (!string.IsNullOrEmpty(role))
                {
                    var result = new AuthenticationResponse();
                    result.UserName = request.UserName;
                    result.Token = tokenService.GenerateToken(request.UserName, role);
                    return result;
                }
                else
                    return null;

            });

        }
    }
}