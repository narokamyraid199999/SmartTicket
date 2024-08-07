using System.ComponentModel.DataAnnotations;

namespace SmartTicket.Models
{
	public class Category
	{
		[Key]
        public int Id { get; set; }

		[Required]
        public string Name { get; set; }

		List<Movie> Movies { get; set; }

    }
}
