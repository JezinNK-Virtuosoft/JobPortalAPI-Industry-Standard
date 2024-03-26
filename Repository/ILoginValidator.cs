using JobPortalAPI_1.ViewModel;

namespace JobPortalAPI_1.Repository
{
    public interface ILoginValidator
    {
        int ValidateUser(LoginCredentials credintials);
        TokenUserDetails GetUserDetailsByID(int UserLoginID);
        int ValidateAdmin(LoginCredentials credintials);
        TokenAdminDetails GetAdminDetailsByID(int AdminID);
    }
}
