using SmartTicket.Models;

namespace SmartTicket.Services
{
    public class CategoryService : IService<Category>
    {
        public CategoryService(SmartTicketContext myDB)
        {
            _myDB = myDB;

        }

        private SmartTicketContext _myDB;

        public async Task commit()
        {
            _myDB.SaveChanges();
        }

        public async Task<bool> Create(Category entity)
        {
            bool state = false;

            try
            {
                _myDB.Categories.Add(entity);
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

            var tempCategory = _myDB.Categories.FirstOrDefault(a => a.Id == id);
            if (tempCategory != null)
            {
                try
                {
                    _myDB.Categories.Remove(tempCategory);
                    commit();
                    state = true;
                }
                catch (Exception ex) { }
            }

            return state;
        }

        public async Task<List<Category>> GetAll()
        {
            return _myDB.Categories.ToList();
        }

        public async Task<Category?> getById(int id)
        {
            var tempActor = _myDB.Categories.FirstOrDefault(x => x.Id == id);

            return tempActor;
        }

        public async Task<bool> Update(int id, Category entity)
        {
            bool state = false;

            var tempCategory = _myDB.Categories.FirstOrDefault(x => x.Id == id);
            if (tempCategory != null)
            {
                tempCategory.Name = entity.Name;
                commit();
            }

            return state;

        }

        public async Task<List<Category>> getMovieByCategoryId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Category>> getMovieACtorForMovie(int id)
        {
            throw new NotImplementedException();
        }

    }
}
