using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartTicket.Models
{
	public class Movie
	{

		[Key]
		public int Id { get; set; }

        public string Name { get; set; }

		public string Description { get; set; }

        public double Price { get; set; }

        public string ImageUrl { get; set; }

        public string TrailerUrl { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

		public string Status { get; set; } = "Available";

		[ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

		[ForeignKey("Cinema")]
		public int? CinemaId { get; set; }

		public Cinema? Cinema { get; set; }

		List<MovieActor> MovieActor { get; set; }  = new List<MovieActor>();

    }
}
