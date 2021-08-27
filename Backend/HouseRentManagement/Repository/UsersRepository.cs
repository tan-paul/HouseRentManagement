using HouseRentManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentManagement.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UserManager<UserAccountModel> _userManager;
        private readonly SignInManager<UserAccountModel> _signInManager;
        private readonly IConfiguration _configuration;

        public UsersRepository(UserManager<UserAccountModel>userManager,SignInManager<UserAccountModel>signInManager,
                    IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        private string ClaimGenerator(string userID, string UserName) {

            var authClaims = new List<Claim>() {
                new Claim(ClaimTypes.Name,UserName),
                new Claim("UserId",userID.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var authSignInKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256Signature)
                );
            //HttpContext.Session.SetString("UserId", JsonConvert.SerializeObject(signIn.User_Name));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> SignUpAsync(SignUpModel signUp) {
            var userObj= new UserAccountModel() {
                FirstName=signUp.FirstName,
                LastName=signUp.LastName,
                Email=signUp.Email,
                PhoneNumber=signUp.PhoneNumber,
                UserName=signUp.UserName
            };
            var result=await _userManager.CreateAsync(userObj, signUp.Password);
            if (!result.Succeeded) return null;

            var userID = _userManager.Users.Where(x => x.UserName == signUp.UserName).Select(x => x.Id).Single();
            return ClaimGenerator(userID, signUp.UserName);

        }

        public async Task<string> SignInAsync(SignInModel signIn) {
            var result = await _signInManager.PasswordSignInAsync(signIn.UserName, signIn.Password, false, false);
            if (!result.Succeeded) return null;

            var userID = _userManager.Users.Where(x => x.UserName == signIn.UserName).Select(x => x.Id).Single();
            return ClaimGenerator(userID,signIn.UserName);
        }
    }
}
