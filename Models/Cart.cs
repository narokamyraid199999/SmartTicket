using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartTicket.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }

        public IdentityUser IdentityUser { get; set; }

        List<cartDetails> cartDetails { get; set; }
    }
}
