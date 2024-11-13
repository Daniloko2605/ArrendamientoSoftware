using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ArrendamientoSoftware.Web.Core;
using ArrendamientoSoftware.Web.Core.Pagination;
using ArrendamientoSoftware.Web.Data;
using ArrendamientoSoftware.Web.Data.Entities;
using ArrendamientoSoftware.Web.DTOs;
using ArrendamientoSoftware.Web.Helpers;
using ClaimsUser = System.Security.Claims.ClaimsPrincipal;

namespace ArrendamientoSoftware.Web.Services
{
    public interface IUsuariosService
    {
        public Task<IdentityResult> AddUserAsync(Usuarios usuario, string password);
        public Task<IdentityResult> ConfirmEmailAsync(Usuarios usuario, string token);
        public Task<Response<Usuarios>> CreateAsync(UsuariosDTO dto);
        public Task<bool> CurrentUserIsAuthorizedAsync(string permission, string module);
        public Task<string> GenerateEmailConfirmationTokenAsync(Usuarios usuario);
        public Task<Response<PaginationResponse<Usuarios>>> GetListAsync(PaginationRequest request);
        public Task<Usuarios> GetUserAsync(string email);
        public Task<Usuarios> GetUserAsync(Guid id);
        public Task<SignInResult> LoginAsync(LoginDTO dto);
        public Task LogoutAsync();
        public Task<Response<Usuarios>> UpdateAsync(UsuariosDTO dto);
    }

    public class UsuariosService : IUsuariosService
    {
        private readonly DataContext _context;
        private readonly SignInManager<Usuarios> _signInManager;
        private readonly UserManager<Usuarios> _userManager;
        private readonly IConverterHelper _converterHelper;
        private IHttpContextAccessor _httpContextAccessor;

        public UsuariosService(DataContext context, SignInManager<Usuarios> signInManager, UserManager<Usuarios> userManager, IConverterHelper converterHelper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _converterHelper = converterHelper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IdentityResult> AddUserAsync(Usuarios usuario, string password)
        {
            return await _userManager.CreateAsync(usuario, password);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(Usuarios usuario, string token)
        {
            return await _userManager.ConfirmEmailAsync(usuario, token);
        }

        public async Task<Response<Usuarios>> CreateAsync(UsuariosDTO dto)
        {
            try
            {
                Usuarios usuario = _converterHelper.ToUser(dto);
                Guid id = Guid.NewGuid();
                usuario.Id = id.ToString();

                IdentityResult result = await AddUserAsync(usuario, dto.Documento);

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

        public async Task<bool> CurrentUserIsAuthorizedAsync(string permission, string module)
        {
            ClaimsUser? claimUser = _httpContextAccessor.HttpContext?.User;

            // Valida si hay sesión
            if (claimUser is null)
            {
                return false;
            }

            string? userName = claimUser.Identity.Name;

            Usuarios? usuario = await GetUserAsync(userName);

            // Valida si usuario existe
            if (usuario is null)
            {
                return false;
            }

            // Valida si es admin
            if (usuario.ArrendamientoSoftwareRole.Name == Env.SUPER_ADMIN_ROLE_NAME)
            {
                return true;
            }

            // Si no es administrador, valida si tiene el permiso
            return await _context.Permissions.Include(p => p.RolePermissions)
                                             .AnyAsync(p => (p.Module == module && p.Name == permission)
                                                        && p.RolePermissions.Any(rp => rp.RoleId == usuario.ArrendamientoSoftwareRoleId));
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(Usuarios usuario)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(usuario);
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

        public async Task<Usuarios> GetUserAsync(string email)
        {
            Usuarios? usuario = await _context.Users.Include(u => u.ArrendamientoSoftwareRole)
                                             .FirstOrDefaultAsync(u => u.Email == email);

            return usuario;
        }

        public async Task<Usuarios> GetUserAsync(Guid id)
        {
            Usuarios? usuario = await _context.Users.Include(u => u.ArrendamientoSoftwareRole)
                                             .FirstOrDefaultAsync(x => x.Id == id.ToString());

            return usuario;
        }

        public async Task<SignInResult> LoginAsync(LoginDTO dto)
        {
            return await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Response<Usuarios>> UpdateAsync(UsuariosDTO dto)
        {
            try
            {
                Usuarios usuario = await GetUserAsync(dto.Id);
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
