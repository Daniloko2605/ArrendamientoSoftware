using ArrendamientoSoftware.Web.Data;
using ArrendamientoSoftware.Web.Helpers;
using Microsoft.EntityFrameworkCore;
using ArrendamientoSoftware.Web.Core;
using ArrendamientoSoftware.Web.Data.Entities;



namespace ArrendamientoSoftware.Web.Services
{
    public interface IUsuariosService
    {
        Task<Response<Usuarios>> CreateAsync(Usuarios model);
        Task<Response<Usuarios>> EditAsync(Usuarios model);
        Task<Response<List<Usuarios>>> GetListAsync();
        Task<Response<Usuarios>> GetOneAsync(int id);
    }

    public class UsuariosService : IUsuariosService
    {
        private readonly DataContext _context;

        public UsuariosService(DataContext context)
        {
            _context = context;
        }

        public async Task<Response<Usuarios>> CreateAsync(Usuarios model)
        {
            try
            {
                await _context.Usuarios.AddAsync(model);
                await _context.SaveChangesAsync();

                return ResponseHelper<Usuarios>.MakeResponseSuccess(model, "Usuario creado con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Usuarios>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Usuarios>> EditAsync(Usuarios model)
        {
            try
            {
                _context.Usuarios.Update(model);
                await _context.SaveChangesAsync();

                return ResponseHelper<Usuarios>.MakeResponseSuccess(model, "Usuario actualizado con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Usuarios>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<List<Usuarios>>> GetListAsync()
        {
            try
            {
                List<Usuarios> users = await _context.Usuarios.ToListAsync();

                return ResponseHelper<List<Usuarios>>.MakeResponseSuccess(users);
            }
            catch (Exception ex)
            {
                return ResponseHelper<List<Usuarios>>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<Usuarios>> GetOneAsync(int id)
        {
            try
            {
                Usuarios? user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

                if (user is null)
                {
                    return ResponseHelper<Usuarios>.MakeResponseFail("El usuario con el id indicado no existe");
                }

                return ResponseHelper<Usuarios>.MakeResponseSuccess(user);
            }
            catch (Exception ex)
            {
                return ResponseHelper<Usuarios>.MakeResponseFail(ex);
            }
        }
    }
}
