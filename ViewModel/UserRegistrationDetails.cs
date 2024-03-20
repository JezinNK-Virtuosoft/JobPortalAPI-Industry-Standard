namespace JobPortalAPI_1.ViewModel
{
    public class UserRegistrationDetails
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
        public string Password { get; set; }
        public int UserTypeID { get; set; }
        
    }
}
