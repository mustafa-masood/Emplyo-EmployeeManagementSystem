using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emplyo_EMS.Server.Models
{

    public class Task
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        public int AssignedBy { get; set; }

        [ForeignKey("AssignedBy")]
        public Employee Assigner { get; set; }

        [Required]
        public int AssignedTo { get; set; }

        [ForeignKey("AssignedTo")]
        public Employee Assignee { get; set; }

        [Required]
        public string TaskDescription { get; set; }

        [Required, StringLength(20)]
        public string Status { get; set; } // Not Started, In Progress, Completed

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }


}
