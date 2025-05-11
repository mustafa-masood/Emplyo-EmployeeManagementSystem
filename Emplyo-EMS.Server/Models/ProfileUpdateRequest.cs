namespace Emplyo_EMS.Server.Models
{
    public class ProfileUpdateRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UpdatedFirstName { get; set; }
        public string UpdatedLastName { get; set; }
        public string Status { get; set; } // "Pending", "Approved", "Rejected"
    }
}
