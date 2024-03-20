using JobPortalAPI_1.ViewModel;

namespace JobPortalAPI_1.Repository
{
    public interface IRegistration
    {
        Task<bool> Register(UserRegistrationDetails registrationDetails);
        
       

    }
}
