using JobPortalAPI_1.Model;
using JobPortalAPI_1.ViewModel;

namespace JobPortalAPI_1.Services
{
    public interface IValidation
    {
        Task<bool> EmailValidator(string Email);
        Task<bool> EmailExists(string Email);
        Task<bool> Credentials(LoginCredintials loginCredintials);
    }
}
