using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Emplyo_EMS.Server.Models 
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required, StringLength(100)]
        public string DepartmentName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation Property
        public ICollection<Employee> Employees { get; set; }
    }
}