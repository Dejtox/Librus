
using GradeSystem.v1.Server.Auth;
using GradeSystem.v1.Server.Data;
using GradeSystem.v1.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GradeSystem.v1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly GradeSystemv1ServerContext _context;
        public AccountController(GradeSystemv1ServerContext context)
        {
            _context = context;
        }

        
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserSession>> LoginAsync([FromBody] LoginRequest loginRequest)
        {
            var jwtAuthMenager = new JwtAuthMenager(_context);
            var userSession = await jwtAuthMenager.GenerateJwtTokenAsync(loginRequest.UserName, loginRequest.Password);
            if (userSession == null)
                return Unauthorized();
            else
                return userSession;
        }
    }
}
