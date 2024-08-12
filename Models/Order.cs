using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTicket.Models
{
	public class Order
	{
		[Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string userId { get; set; }

        public IdentityUser User { get; set; }

        List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }
}
