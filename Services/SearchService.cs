using Microsoft.EntityFrameworkCore;
using SmartTicket.Models;

namespace SmartTicket.Services
{
    public class SearchService:ISearch
    {
        public SearchService(SmartTicketContext myDB)
        {
            _myDB = myDB;

        }

        private SmartTicketContext _myDB;


        public async Task<List<Movie>> Search(string name)
        {
            var searchedMovie = _myDB.Movies.Include(x => x.Cinema).Include(x => x.Category).Where(x => x.Name.Contains(name.Trim())).ToList();
            return searchedMovie;
        }
    }
}
