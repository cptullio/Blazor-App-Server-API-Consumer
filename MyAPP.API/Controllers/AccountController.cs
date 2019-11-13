using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAPP.API.Services;


namespace MyAPP.API.Controllers
{
    
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        
        private ITokenService tokenService;
        public AccountController([FromServices]ITokenService _tokenService)
        {
            tokenService = _tokenService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult Login([FromBody]string userName){
            
            var role = UserService.GetRoleByUserName(userName);

            if (string.IsNullOrEmpty(role))
                return NotFound(new {message = "User Not Found"});
            
            var token = tokenService.GenerateToken(userName,role);
            return Ok(new {
                user = userName,
                token = token
            });

        }

        [HttpGet]
        [Route("admin")]
        [Authorize(Roles="admin")]        
        public ActionResult CallAdmin(){
            return Ok(new {
                user = User.Identity.Name
            });

        }
       
        [HttpGet]
        [Route("user")]
        [Authorize(Roles="user,admin")]        
        public ActionResult CallUser(){
            return Ok(new {
                user = User.Identity.Name
            });

        }



    }
}