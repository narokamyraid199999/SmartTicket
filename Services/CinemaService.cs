using SmartTicket.Models;

namespace SmartTicket.Services
{
    public class CinemaService : IService<Cinema>
    {
        public CinemaService(SmartTicketContext myDB)
        {
            _myDB = myDB;

        }

        private SmartTicketContext _myDB;

        public async Task commit()
        {
            _myDB.SaveChanges();
        }

        public async Task<bool> Create(Cinema entity)
        {
            bool state = false;

            try
            {
                _myDB.Cinemas.Add(entity);
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

            var tempCinema = _myDB.Cinemas.FirstOrDefault(a => a.Id == id);
            if (tempCinema != null)
            {
                try
                {
                    _myDB.Cinemas.Remove(tempCinema);
                    commit();
                    state = true;
                }
                catch (Exception ex) { }
            }

            return state;
        }

        public async Task<List<Cinema>> GetAll()
        {
            return _myDB.Cinemas.ToList();
        }

        public async Task<Cinema?> getById(int id)
        {
            var tempCinema = _myDB.Cinemas.FirstOrDefault(x => x.Id == id);

            return tempCinema;
        }

        public async Task<bool> Update(int id, Cinema entity)
        {
            bool state = false;

            var tempCinema = _myDB.Cinemas.FirstOrDefault(a => a.Id == id);
            if (tempCinema != null)
            {
                tempCinema.Name = entity.Name;
                tempCinema.Address = entity.Address;
                tempCinema.Description = entity.Description;
                tempCinema.Logo = entity.Logo;
                commit();
            }

            return state;

        }

        public async Task<List<Cinema>> getMovieByCategoryId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Cinema>> getMovieACtorForMovie(int id)
        {
            throw new NotImplementedException();
        }

    }
}
