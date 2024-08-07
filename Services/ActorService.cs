using Microsoft.EntityFrameworkCore;
using SmartTicket.Models;

namespace SmartTicket.Services
{
    public class ActorService : IService<Actor>
    {
        public ActorService(SmartTicketContext myDB)
        {
            _myDB = myDB;

        }

        private SmartTicketContext _myDB;

        public async Task commit()
        {
            _myDB.SaveChanges();
        }

        public async Task<bool> Create(Actor entity)
        {
            bool state = false;

            try
            {
                _myDB.Actors.Add(entity);
                commit();
                state = true;
            }catch (Exception ex)
            {
                state = false;
            }

            return state;

        }

        public async Task<bool> Delete(int id)
        {
            bool state = false;

            var tempActor = _myDB.Actors.FirstOrDefault(a => a.Id == id);
            if (tempActor != null)
            {
                try
                {
                    _myDB.Actors.Remove(tempActor);
                    commit();
                    state = true;
                }
                catch (Exception ex){}
            }

            return state;
        }

        public async Task<List<Actor>> GetAll()
        {
            return _myDB.Actors.ToList();
        }

        public async Task<Actor?> getById(int id)
        {
            var tempActor = _myDB.Actors.FirstOrDefault(x=>x.Id == id);

            return tempActor;
        }

        public async Task<bool> Update(int id, Actor entity)
        {
            bool state = false;

            var tempActor = _myDB.Actors.FirstOrDefault(x => x.Id == id);
            if (tempActor != null)
            {
                tempActor.Description = entity.Description;
                tempActor.FirstName= entity.FirstName;
                tempActor.LastName= entity.LastName;
                tempActor.Image = entity.Image;

                commit();
            }

            return state;

        }


        public async Task<List<Actor>> getMovieByCategoryId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Actor>> getMovieACtorForMovie(int id)
        {
            throw new NotImplementedException();
        }
    }
}
