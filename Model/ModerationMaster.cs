namespace JobPortalAPI_1.Model
{
    public class ModerationMaster
    {
        public int ModerationID { get; set; }
        public string ModerationName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}
