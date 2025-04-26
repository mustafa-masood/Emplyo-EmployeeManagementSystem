using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Emplyo_EMS.Server.Models
{
    public class Document
    {
        [Key]
        public int DocumentId { get; set; }

        // Foreign Key to LeaveRequest
        [Required]
        public int LeaveRequestId { get; set; }

        [ForeignKey("LeaveRequestId")]
        public LeaveRequest LeaveRequest { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; }

        [Required]
        [StringLength(255)]
        public string FilePath { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.Now;

        public bool IsApproved { get; set; } = false;

        // Employee who approves the document
        public int? ApprovedBy { get; set; }

        [ForeignKey("ApprovedBy")]
        public Employee ApprovedByEmployee { get; set; }

        public DateTime? ApprovalDate { get; set; }
    }
}
