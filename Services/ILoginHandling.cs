using JobPortalAPI_1.ViewModel;

namespace JobPortalAPI_1.Services
{
    public interface ILoginHandling
    {
        Task<string> UserLoginHandler(LoginCredintials credintials);
    }
}
