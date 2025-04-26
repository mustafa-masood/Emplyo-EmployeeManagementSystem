using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Emplyo_EMS.Server.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }

        // Foreign Key
        [Required]
        public int RoleId { get; set; }

        // Navigation Property
        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation property for linked Employee (if any)
        public Employee Employee { get; set; }
    }
}