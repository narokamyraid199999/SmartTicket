using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTicket.Models
{
	public class OrderDetails
	{
		[Key]
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int orderId { get; set; }

        public Order Order { get; set; }

		[ForeignKey("Movie")]
		public int movieId { get; set; }

        public Movie Movie { get; set; }

    }
}
