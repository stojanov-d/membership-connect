using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BE_membership_connect.Models
{
    public class UserMembership
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int MembershipId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [ForeignKey("UserId")]
        public required virtual AppUser User { get; set; }

        [ForeignKey("MembershipId")]
        public required virtual Membership Membership { get; set; }

        [ForeignKey("UpgradedFromMembershipId")]
        public virtual UserMembership? UpgradedFromMembership { get; set; }


    }
}