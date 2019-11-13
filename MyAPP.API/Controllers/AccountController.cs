using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAPP.API.Features.Commands;
using MyAPP.API.Services;


namespace MyAPP.API.Controllers
{

    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody]AuthenticationRequest request)
        {
            var response = await _mediator.Send(request);
            if (response == null)
                return NotFound(new { message = "Invalid Login" });
            return Ok(response);
        }

        [HttpGet]
        [Route("admin")]
        [Authorize(Roles = "admin")]
        public ActionResult CallAdmin()
        {
            return Ok(new
            {
                user = User.Identity.Name
            });

        }

        [HttpGet]
        [Route("user")]
        [Authorize(Roles = "user,admin")]
        public ActionResult CallUser()
        {
            return Ok(new
            {
                user = User.Identity.Name
            });

        }



    }
}