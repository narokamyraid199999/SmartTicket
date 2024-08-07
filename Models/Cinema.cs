using System.ComponentModel.DataAnnotations;

namespace SmartTicket.Models
{
	public class Cinema
	{
		[Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public string Address { get; set; }

		[Required]
		public string Logo { get; set; }

		List<Movie> Movies { get; set; }

    }
}
