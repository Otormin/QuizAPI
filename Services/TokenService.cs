using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using QuizWebApiProject.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuizWebApiProject.Services
{
    public class TokenService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        //IConfiguration is anything you put inside the appsettigs.json
        public TokenService(UserManager<User> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        //a token contains 3 parts: the header, the payload, the signature
        public async Task<string> GenerateToken(User user)
        {
            //claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //signature
            //SymmetricSecurityKey is the same key that is used to sign the key is the same key that is used to decrypt the key
            //make sure the SymmetricSecurityKey never leaves the server and is protected on the server in a safe plce
            //anyone that has access to the SymmetricSecurityKey can pretend to be any user in the application including the admin
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:TokenKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);


            //Jwttoken
            var tokenOptions = new JwtSecurityToken
            (
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
