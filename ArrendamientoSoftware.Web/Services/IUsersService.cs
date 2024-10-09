
using ArrendamientoSoftware.Web.Core;
using ArrendamientoSoftware.Web.Data;
using ArrendamientoSoftware.Web.Data.Entities;

namespace ArrendamientoSoftware.Web.Services
{

    public interface IUsersService
    {
        public Task<Response<List<User>>> GetListAsync();

    }

    public class UsersService : IUsersService
    {
        private readonly DataContext _context;

        public UsersService(DataContext context)
        {
            _context = context;

        }
    }

    public async Task<Response<List<User>> GetListAsync()
    {
        try
        {

        }
        catch(Exception ex) {
        {
            
        }
    }

        




}
