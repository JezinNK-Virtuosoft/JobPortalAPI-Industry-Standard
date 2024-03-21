
using JobPortalAPI_1.Model;

namespace JobPortalAPI_1.Services
{
    public interface IRetrivingData
    {
        Task<IEnumerable<UserDetails>> GetUsers();
    }
}
