using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emplyo_EMS.Server.Models
{

    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }

        // Foreign Key to Employee
        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }

        [Required, StringLength(20)]
        public string Status { get; set; } // Present, Absent, Late

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}
