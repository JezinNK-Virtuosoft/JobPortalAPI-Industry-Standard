using JobPortalAPI_1.Model;
using JobPortalAPI_1.Repository;

namespace JobPortalAPI_1.Services
{
    public class RetrievingData:IRetrivingData
    {
        private readonly IAdminDataHandling _adminDatahandling;
        
        public RetrievingData(IAdminDataHandling adminDataHandling)
        {
                _adminDatahandling = adminDataHandling;
                
        }
        public async Task<IEnumerable<UserDetails>> GetUsers() 
        {   
            var userLists=await _adminDatahandling.GetUserDetails();
            if (userLists!=null)
            {
                return userLists; 
            }
            return Enumerable.Empty<UserDetails>();
        }

    }
}
