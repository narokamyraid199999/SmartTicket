using System.ComponentModel.DataAnnotations;

namespace SmartTicket.Models
{
	public class Actor
	{

		[Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		public string Image { get; set; }

		[Required]
		public string Description { get; set; }

		List<MovieActor> MovieActors { get; set; }
	}
}
