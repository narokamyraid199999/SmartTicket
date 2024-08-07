using Microsoft.EntityFrameworkCore;
using SmartTicket.Models;

namespace SmartTicket.Services
{
    public class MovieActorService : IService<MovieActor>
    {
        public MovieActorService(SmartTicketContext myDB)
        {
            _myDB = myDB;

        }

        private SmartTicketContext _myDB;

        public async Task commit()
        {
            _myDB.SaveChanges();
        }

        public async Task<bool> Create(MovieActor entity)
        {
            bool state = false;

            try
            {
                _myDB.MovieActors.Add(entity);
                commit();
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

            var tempActor = _myDB.MovieActors.FirstOrDefault(a => a.Id == id);
            if (tempActor != null)
            {
                try
                {
                    _myDB.MovieActors.Remove(tempActor);
                    commit();
                    state = true;
                }
                catch (Exception ex) { }
            }

            return state;
        }

        public async Task<List<MovieActor>> GetAll()
        {
            return _myDB.MovieActors.Include(x => x.Actor).Include(x => x.Movie).ToList();
        }

        public async Task<MovieActor?> getById(int id)
        {
            var tempActor = _myDB.MovieActors.FirstOrDefault(x => x.Id == id);

            return tempActor;
        }

        public async Task<bool> Update(int id, MovieActor entity)
        {
            bool state = false;

            var tempActor = _myDB.MovieActors.FirstOrDefault(x => x.Id == id);
            if (tempActor != null)
            {
                tempActor.MovieId = entity.MovieId;
                tempActor.ActorId = entity.ActorId;
                commit();
            }

            return state;

        }

        public async Task<List<MovieActor>> getMovieByCategoryId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MovieActor>> getMovieACtorForMovie(int id)
        {
            var tempActors = _myDB.MovieActors.Where(x => x.MovieId == id).Include(x => x.Actor).ToList();
            return tempActors;
        }
    }
}
