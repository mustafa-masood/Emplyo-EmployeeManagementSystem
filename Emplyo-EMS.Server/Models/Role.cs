using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Emplyo_EMS.Server.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required, StringLength(50)]
        public string RoleName { get; set; }

        // Navigation Property
        public ICollection<User> Users { get; set; }

    }
}