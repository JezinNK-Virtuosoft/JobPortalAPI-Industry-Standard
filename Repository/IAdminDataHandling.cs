using JobPortalAPI_1.Model;

namespace JobPortalAPI_1.Repository
{
    public interface IAdminDataHandling
    {
        Task<IEnumerable<UserDetails>> GetUserDetails();
    }
}
