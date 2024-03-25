using Microsoft.AspNetCore.Identity;

namespace JobPortalAPI_1.ViewModel
{
    public class TokenAdminDetails
    {
        public int AdminID { get; set; }
        public string Email { get; set; }
        public int UserTypeID { get; set; }
    }
}
