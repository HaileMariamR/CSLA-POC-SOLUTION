using Data_Layer.Interface;
using Data_Layer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace cslabwasmpoc.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
{

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public string Get()
        {
            return "Auth Running";
        }
            
        [HttpPost]
        public async Task<LoginResult> Post([FromBody] LoginModel loginModel )
        {
            try
            {
                AuthModel authModel = await _authService.AuthenticateUser(loginModel.userName, loginModel.passWord);

                if (authModel.isAuthenticated)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sampleScretKey@1234"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokeOptions = new JwtSecurityToken(
                        issuer: "http://localhost:5187",
                        audience: "https://localhost:5188",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    var refreshToken = _authService.GenerateRefreshToken();


                    return new LoginResult { Message = "succesfully logged in", JwtToken = tokenString, UserName = loginModel.userName, Role = authModel?.CurrentUser?.Role , RefreshToken = refreshToken };

                }
                return new LoginResult { Message = "user not found" }; 

            }
            catch (Exception ex)
            {
                return new LoginResult { Message = ex.Message };
            }
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenModel refreshTokenModel)
        {
            if (refreshTokenModel is null)
            {
                return BadRequest(new  { authSucess = false, ErrorMessage = "Invalid client request" });
            }
            var currentPrincipal = await _authService.GetCurrentUserFromExpiredToken(refreshTokenModel.Token , refreshTokenModel.UserName);
            var username =  currentPrincipal.UserName;
            var user = await _authService.getCurrentUser(username);
            if (user == null)
                return BadRequest(new  { authSucess = false, ErrorMessage = "Invalid client request" });
           
            var tokenOptions =  _authService.GenerateToken();
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            var refreshToken = _authService.GenerateRefreshToken();

            return Ok(new LoginResult { Message = "succesfully refreshed", JwtToken = token, UserName = username, Role = currentPrincipal.Role, RefreshToken = refreshToken });
            
        }



    }
}
