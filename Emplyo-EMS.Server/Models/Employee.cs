using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Emplyo_EMS.Server.Models
{

    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        // Foreign Key to User
        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        // Foreign Key to Department
        public int? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        public DateTime JoinDate { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Self-referencing FK for Manager
        public int? ManagerId { get; set; }

        [ForeignKey("ManagerId")]
        public Employee Manager { get; set; }

        // Navigation properties
        public ICollection<Employee> Subordinates { get; set; }
        public ICollection<Attendance> AttendanceRecords { get; set; }
        public ICollection<LeaveRequest> Leaves { get; set; }
        public ICollection<Payroll> Payrolls { get; set; }
        public ICollection<Performance> Performances { get; set; }
        public ICollection<Task> AssignedTasks { get; set; }       // As assignee
        public ICollection<Task> GivenTasks { get; set; }          // As assigner
        public ICollection<Document> Documents { get; set; }
    }

}
