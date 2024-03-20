using JobPortalAPI_1.ViewModel;

namespace JobPortalAPI_1.Repository
{
    public interface IRegistration
    {
        Task<bool> Register(UserRegistrationDetails registrationDetails);
        Task<string> ToHashSHA1(string Password);
        Task<string> GenerateSalt();
       

    }
}
