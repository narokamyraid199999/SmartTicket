using SmartTicket.Models;

namespace SmartTicket.Services
{
    public interface IService<T> where T : class
    {
     
        public Task<List<T>> GetAll();

        public Task<T?> getById(int id);

        public Task<bool> Create(T entity);

        public Task<bool> Update(int id, T entity);

        public Task<bool> Delete(int id);

        public Task commit();

        public Task<List<T>> getMovieByCategoryId(int id);

        public Task<List<T>> getMovieACtorForMovie(int id);

    }
}
