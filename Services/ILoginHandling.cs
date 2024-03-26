using JobPortalAPI_1.ViewModel;

namespace JobPortalAPI_1.Services
{
    public interface ILoginHandling
    {
        Task<string> UserLoginHandler(LoginCredentials credintials);
        TokenAdminDetails ValidateAdmin(LoginCredentials credintials);
        Task<string> AdminLoginHandler(LoginCredentials credintials);
    }
}
