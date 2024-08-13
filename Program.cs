using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartTicket.Models;
using SmartTicket.Services;

namespace SmartTicket
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddScoped<IService<Actor>, ActorService>();
			builder.Services.AddScoped<IService<Category>, CategoryService>();
			builder.Services.AddScoped<IService<Movie>, MovieService>();
			builder.Services.AddScoped<IService<MovieActor>, MovieActorService>();
			builder.Services.AddScoped<IService<Cinema>, CinemaService>();
			builder.Services.AddScoped<ISearch, SearchService>();
			builder.Services.AddScoped<IEmailService, MailService>();


			builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("smtp"));

            builder.Services.AddDbContext<SmartTicketContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDb"));
			});

			builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<SmartTicketContext>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			//app.UseAuthentication();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
