namespace JobPortalAPI_1.Model
{
    public class UserLoginDetails
    {
        public int UserLoginID { get; set; }
        public string Password { get; set; }
        public int UserTypeID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
