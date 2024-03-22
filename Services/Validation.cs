using JobPortalAPI_1.Controllers;
using JobPortalAPI_1.Model;
using JobPortalAPI_1.Repository;
using JobPortalAPI_1.ViewModel;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace JobPortalAPI_1.Services
{
    public  class Validation:IValidation
    {
        private  readonly IRegistration _registration;
        public Validation(IRegistration registration)
        {
                _registration = registration;
        }
        //Email Regex Validation
        public  async Task<bool> EmailValidator(string Email)
        {
            if ((string.IsNullOrEmpty(Email)))
            {
                return false;
            }
            Email = Email.Trim();
            Regex EmailRegex = new Regex("^(?=.{1,50}$)[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");
            
            return  EmailRegex.IsMatch(Email);
        }
        //Checks whether the Email Exists
        public  async Task<bool> EmailExists(string Email)
        {
            
            if ((string.IsNullOrEmpty(Email)))
            {
                return false ;
            }
            Email = Email.Trim();
            if (await EmailValidator(Email))
            {
               return await _registration.IsUserEmailExists(Email);
            }
            return false;
        }

        //Check wheter the Credentials are of correct length
        public async Task<bool> Credentials(LoginCredintials loginCredintials) 
        {   
            
            var Length = new Regex("^(?=.{1,64}$)");
                        
            return await EmailValidator(loginCredintials.Email) && Length.IsMatch(loginCredintials.Password);
        }
        
        

    }
}
