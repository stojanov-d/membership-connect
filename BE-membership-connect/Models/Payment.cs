using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BE_membership_connect.Models
{
    public class Payment
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int MembershipId { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Required]
        public string PaymentStatus { get; set; } = "Pending";

        [ForeignKey("UserId")]
        public required virtual AppUser User { get; set; }

        [ForeignKey("MembershipId")]
        public required virtual Membership Membership { get; set; }

    }
}