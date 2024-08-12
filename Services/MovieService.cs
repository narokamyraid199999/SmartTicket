using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartTicket.Models;

namespace SmartTicket.Services
{
    public class MovieService : IService<Movie>
    {
        public MovieService(SmartTicketContext myDB)
        {
            _myDB = myDB;

        }

        private SmartTicketContext _myDB;

        public async Task commit()
        {
            _myDB.SaveChanges();
        }

        public async Task<bool> Create(Movie entity)
        {
            bool state = false;

            try
            {
                _myDB.Movies.Add(entity);
				await commit();
				state = true;
            }
            catch (Exception ex)
            {
                state = false;
            }

            return state;

        }

        public async Task<bool> Delete(int id)
        {
            bool state = false;

            var tempMovie = _myDB.Movies.FirstOrDefault(a => a.Id == id);
            if (tempMovie != null)
            {
                try
                {
                    _myDB.Movies.Remove(tempMovie);
					await commit();
					state = true;
                }
                catch (Exception ex) { }
            }

            return state;
        }

        public async Task<List<Movie>> GetAll()
        {
            return _myDB.Movies.Include(x => x.Cinema).Include(x => x.Category).ToList();
        }

        public async Task<Movie?> getById(int id)
        {
            var tempMovie = _myDB.Movies.Include(x=>x.Category).Include(x=>x.Cinema).FirstOrDefault(x => x.Id == id);

            return tempMovie;
        }

        public async Task<bool> Update(int id, Movie entity)
        {
            bool state = false;


		    var tempMovie = _myDB.Movies.FirstOrDefault(x => x.Id == id);
		    if (tempMovie != null)
		    {
			    tempMovie.Description = entity.Description;
			    tempMovie.Name = entity.Name;
			    tempMovie.Status = entity.Status;
			    tempMovie.Price = entity.Price;
			    tempMovie.StartDate = entity.StartDate;
			    tempMovie.EndDate = entity.EndDate;
			    tempMovie.CategoryId = entity.CategoryId;
			    tempMovie.CinemaId=entity.CinemaId;
			    await commit();
		    }
			return state;
        }

        public async Task<List<Movie>> getMovieByCategoryId(int id)
        {
            var categoryMovies = _myDB.Movies.Include(x => x.Cinema).Include(x => x.Category).Where(x => x.CategoryId == id).ToList();
            return categoryMovies;
        }

        public async Task<List<Movie>> getMovieACtorForMovie(int id)
        {
            throw new NotImplementedException();
        }

    }
}
