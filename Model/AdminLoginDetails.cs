using System.Reflection.Metadata.Ecma335;

namespace JobPortalAPI_1.Model
{
    public class AdminLoginDetails
    {
        public int AdminLoginID { get; set; }
        public string Password { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int UserTypeID { get; set; }
    }
}
