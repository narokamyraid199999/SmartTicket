using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTicket.Models
{
    public class cartDetails
    {
        public int Id { get; set; }

        [ForeignKey("cart")]
        public int cartId { get; set; }

        public Cart cart { get; set; }

        [ForeignKey("Movie")]
        public int movieId { get; set; }

        public Movie Movie { get; set; }
    }
}
