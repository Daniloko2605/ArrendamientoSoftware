using ArrendamientoSoftware.Web.Data;
using ArrendamientoSoftware.Web.Helpers;
using Microsoft.EntityFrameworkCore;
using ArrendamientoSoftware.Web.Core;
using ArrendamientoSoftware.Web.Data.Entities;



namespace ArrendamientoSoftware.Web.Services
{
    public interface IUsersServices
    {
        Task<Response<User>> CreateAsync(User model);
        Task<Response<User>> EditAsync(User model);
        Task<Response<List<User>>> GetListAsync();
        Task<Response<User>> GetOneAsync(int id);
    }

    public class UserService : IUsersServices
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<User>> CreateAsync(User model)
        {
            try
            {
                await _context.Users.AddAsync(model);
                await _context.SaveChangesAsync();

                return ResponseHelper<User>.MakeResponseSucess(model, "Usuario creado con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<User>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<User>> EditAsync(User model)
        {
            try
            {
                _context.Users.Update(model);
                await _context.SaveChangesAsync();

                return ResponseHelper<User>.MakeResponseSucess(model, "Usuario actualizado con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<User>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<List<User>>> GetListAsync()
        {
            try
            {
                List<User> users = await _context.Users.ToListAsync();

                return ResponseHelper<List<User>>.MakeResponseSucess(users);
            }
            catch (Exception ex)
            {
                return ResponseHelper<List<User>>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<User>> GetOneAsync(int id)
        {
            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (user is null)
                {
                    return ResponseHelper<User>.MakeResponseFail("El usuario con el id indicado no existe");
                }

                return ResponseHelper<User>.MakeResponseSucess(user);
            }
            catch (Exception ex)
            {
                return ResponseHelper<User>.MakeResponseFail(ex);
            }
        }
    }
}
