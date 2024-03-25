using JobPortalAPI_1.ViewModel;

namespace JobPortalAPI_1.Repository
{
    public interface ILoginValidator
    {
        int ValidateUser(LoginCredintials credintials);
        TokenUserDetails GetUserDetailsByID(int UserLoginID);
        int ValidateAdmin(LoginCredintials credintials);
        TokenAdminDetails GetAdminDetailsByID(int AdminID);
    }
}
