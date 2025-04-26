using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emplyo_EMS.Server.Models
{
    public class LeaveRequest
    {
        [Key]
        public int LeaveRequestId { get; set; }

        // Foreign Key to Employee who is requesting the leave
        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        // Leave Type - Stored as string, e.g., "Sick Leave", "Casual Leave"
        [Required, StringLength(50)]
        public string LeaveType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required, StringLength(20)]
        public string Status { get; set; } // E.g., Pending, Approved, Rejected

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Foreign Keys for Request and Approval (both are related to Employees)
        public int? RequestedBy { get; set; } // Who requested the leave
        public int? ApprovedBy { get; set; } // Who approved the leave

        // Navigation Properties for Request and Approval Employees
        [ForeignKey("RequestedBy")]
        public Employee RequestedByEmployee { get; set; }

        [ForeignKey("ApprovedBy")]
        public Employee ApprovedByEmployee { get; set; }

        // Optional: Date when the leave was approved
        public DateTime? ApprovalDate { get; set; }
    }
}
