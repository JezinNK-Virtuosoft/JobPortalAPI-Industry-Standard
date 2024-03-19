using System.Reflection.Metadata.Ecma335;

namespace JobPortalAPI_1.Model
{
    public class UserDetails
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public int UserLoginID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int Status { get; set; }
        public string Remark { get; set; }
    }
}
