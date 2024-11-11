using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ArrendamientoSoftware.Web.Core;
using ArrendamientoSoftware.Web.Core.Pagination;
using ArrendamientoSoftware.Web.Data;
using ArrendamientoSoftware.Web.Data.Entities;
using ArrendamientoSoftware.Web.DTOs;
using ArrendamientoSoftware.Web.Helpers;

namespace ArrendamientoSoftware.Web.Services
{
    public interface IUsuariosService
    {
        public Task<IdentityResult> AddUsuariosAsync(Usuarios usuario, string password);
        public Task<IdentityResult> ConfirmEmailAsync(Usuarios usuario, string token);
        public Task<Response<Usuarios>> CreateAsync(UsuariosDTO dto);
        public Task<string> GenerateEmailConfirmationTokenAsync(Usuarios usuario);
        public Task<Response<PaginationResponse<Usuarios>>> GetListAsync(PaginationRequest request);
        public Task<Usuarios> GetUsuariosAsync(string email);
        public Task<Usuarios> GetUsuariosAsync(Guid id);
        public Task<SignInResult> LoginAsync(LoginDTO dto);
        public Task LogoutAsync();
        public Task<IdentityResult> UpdateUsuariosAsync(Usuarios usuario);
        public Task<Response<Usuarios>> UpdateUsuariosAsync(UsuariosDTO dto);
    }

    public class UsuariosService : IUsuariosService
    {
        private readonly DataContext _context;
        private readonly SignInManager<Usuarios> _signInManager;
        private readonly UserManager<Usuarios> _usuarioManager;
        private readonly IConverterHelper _converterHelper;

        public UsuariosService(DataContext context, SignInManager<Usuarios> signInManager, UserManager<Usuarios> usuarioManager, IConverterHelper converterHelper)
        {
            _context = context;
            _signInManager = signInManager;
            _usuarioManager = usuarioManager;
            _converterHelper = converterHelper;
        }

        public async Task<IdentityResult> AddUsuariosAsync(Usuarios usuario, string password)
        {
            return await _usuarioManager.CreateAsync(usuario, password);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(Usuarios usuario, string token)
        {
            return await _usuarioManager.ConfirmEmailAsync(usuario, token);
        }

        public async Task<Response<Usuarios>> CreateAsync(UsuariosDTO dto)
        {
            try
            {
                Usuarios usuario = _converterHelper.ToUser(dto);
                Guid id = Guid.NewGuid();
                usuario.Id = id.ToString();

                IdentityResult result = await AddUsuariosAsync(usuario, dto.Documento);

                // TODO: Ajustar cuando se realize funcionalidad para envío de Email
                string token = await GenerateEmailConfirmationTokenAsync(usuario);
                await ConfirmEmailAsync(usuario, token);

                return ResponseHelper<Usuarios>.MakeResponseSuccess(usuario, "Usuario creado con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Usuarios>.MakeResponseFail(ex);
            }
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(Usuarios usuario)
        {
            return await _usuarioManager.GenerateEmailConfirmationTokenAsync(usuario);
        }

        public async Task<Response<PaginationResponse<Usuarios>>> GetListAsync(PaginationRequest request)
        {
            try
            {
                IQueryable<Usuarios> query = _context.Users.AsQueryable()
                                                       .Include(u => u.ArrendamientoSoftwareRole);

                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    query = query.Where(s => s.Nombre.ToLower().Contains(request.Filter.ToLower())
                                            || s.Apellido.ToLower().Contains(request.Filter.ToLower())
                                            || s.Documento.ToLower().Contains(request.Filter.ToLower())
                                            || s.Email.ToLower().Contains(request.Filter.ToLower())
                                            || s.PhoneNumber.ToLower().Contains(request.Filter.ToLower()));
                }

                PagedList<Usuarios> list = await PagedList<Usuarios>.ToPagedListAsync(query, request);

                PaginationResponse<Usuarios> result = new PaginationResponse<Usuarios>
                {
                    List = list,
                    TotalCount = list.TotalCount,
                    RecordsPerPage = list.RecordsPerPage,
                    CurrentPage = list.CurrentPage,
                    TotalPages = list.TotalPages,
                    Filter = request.Filter
                };

                return ResponseHelper<PaginationResponse<Usuarios>>.MakeResponseSuccess(result, "Usuarios obtenidos con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<PaginationResponse<Usuarios>>.MakeResponseFail(ex);
            }
        }

        public async Task<Usuarios> GetUsuariosAsync(string email)
        {
            Usuarios? usuario = await _context.Users.Include(u => u.ArrendamientoSoftwareRole)
                                             .FirstOrDefaultAsync(u => u.Email == email);

            return usuario;
        }

        public async Task<Usuarios> GetUsuariosAsync(Guid id)
        {
            return await _context.Users.Include(u => u.ArrendamientoSoftwareRole)
                                             .FirstOrDefaultAsync(u => u.Id == id.ToString());
        }

        public async Task<SignInResult> LoginAsync(LoginDTO dto)
        {
            return await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> UpdateUsuariosAsync(Usuarios usuario)
        {
            return await _usuarioManager.UpdateAsync(usuario);
        }

        public async Task<Response<Usuarios>> UpdateUsuariosAsync(UsuariosDTO dto)
        {
            try
            {
                Usuarios usuario = await GetUsuariosAsync(dto.Id);
                usuario.PhoneNumber = dto.Telefono;
                usuario.Documento = dto.Documento;
                usuario.Nombre = dto.Nombre;
                usuario.Apellido = dto.Apellido;
                usuario.ArrendamientoSoftwareRoleId = dto.ArrendamientoSoftwareRoleId;

                _context.Users.Update(usuario);

                await _context.SaveChangesAsync();

                return ResponseHelper<Usuarios>.MakeResponseSuccess(usuario, "Usuario actualizado con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<Usuarios>.MakeResponseFail(ex);
            }
        }
    }
}

