﻿using JobPortalAPI_1.Repository;
using JobPortalAPI_1.ViewModel;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Transactions;

namespace JobPortalAPI_1.Services
{
    public class LoginHandling:ILoginHandling
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginValidator _loginValidator;
        private readonly IValidation _validation;

        public LoginHandling(IConfiguration configuration,ILoginValidator loginValidator,IValidation validation)
        {
            _configuration = configuration;
            _loginValidator = loginValidator;
            _validation = validation;
        }
        public TokenUserDetails ValidateUser(LoginCredintials credintials)
        {
            if (credintials!=null)
            {
                int UserLoginID = _loginValidator.ValidateUser(credintials);
                if (UserLoginID >-1)
                {
                    TokenUserDetails tkUserDetails = _loginValidator.GetUserDetailsByID(UserLoginID);
                    return tkUserDetails;
                }
            } 
            
            return new TokenUserDetails();    
        }
        public async Task<string> GenerateUserToken(TokenUserDetails userdetails)
        {
            return await Task.Run(() =>
            {
                var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier,userdetails.FirstName),
                new Claim(ClaimTypes.Email,userdetails.Email),
                new Claim(ClaimTypes.Role,userdetails.UserTypeID.ToString())
            };
                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims,
                    expires: DateTime.Now.AddDays(1), signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(token);
            });
            
        }
        
        public async Task<string> UserLoginHandler(LoginCredintials credintials) 
        {
            credintials.Email = credintials.Email.Trim();
            credintials.Password = credintials.Password.Trim();
            if (await _validation.Credentials(credintials))
            {
                credintials.Password=await PasswordHandler.ToHashSHA1(credintials.Password);
                TokenUserDetails tokenUserDetails =  ValidateUser(credintials);
                if (tokenUserDetails!=null)
                {
                    var token = await GenerateUserToken(tokenUserDetails);
                    return token.ToString();
                }
            }
            return null;
        }
               
    }
}
