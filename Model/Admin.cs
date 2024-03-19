using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace JobPortalAPI_1.Model
{
    public class Admin
    {
        public int AdminID { get; set; }
        public string Email { get; set; }
        public string EmployeeNO { get; set; }
        public string Designation { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
