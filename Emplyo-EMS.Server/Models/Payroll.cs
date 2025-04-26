using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emplyo_EMS.Server.Models
{

    public class Payroll
    {
        [Key]
        public int PayrollId { get; set; }

        // Foreign Key to Employee
        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        [Required]
        public DateTime PayPeriodStart { get; set; }

        [Required]
        public DateTime PayPeriodEnd { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal GrossSalary { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Deductions { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal NetSalary { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}
