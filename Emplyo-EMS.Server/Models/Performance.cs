using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emplyo_EMS.Server.Models
{

    public class Performance
    {
        [Key]
        public int PerformanceId { get; set; }

        // Foreign Key to Employee (the one being reviewed)
        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        // Foreign Key to Manager (the reviewer)
        public int? ManagerId { get; set; }

        [ForeignKey("ManagerId")]
        public Employee Manager { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }  // Scale: 1 to 5

        public string Comments { get; set; }

        public DateTime ReviewDate { get; set; } = DateTime.Now;
    }

}
