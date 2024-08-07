using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SmartTicket.Models
{
	public class SmartTicketContext:IdentityDbContext
	{
		public SmartTicketContext(DbContextOptions<SmartTicketContext> options):base(options) { }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Cinema> Cinemas { get; set; }

		public DbSet<Movie> Movies { get; set; }

        public DbSet<MovieActor> MovieActors { get; set; }

    }
}
