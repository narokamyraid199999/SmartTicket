using SmartTicket.Models;

namespace SmartTicket.Services
{
    public interface ISearch
    {

        public Task<List<Movie>> Search(string name);

    }
}
