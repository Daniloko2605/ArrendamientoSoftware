using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using ArrendamientoSoftware.Web.Core.Pagination;
using ArrendamientoSoftware.Web.Data.Entities;
using ArrendamientoSoftware.Web.Data;
using ArrendamientoSoftware.Web.Helpers;
using ArrendamientoSoftware.Web.Core;
using ArrendamientoSoftware.Web.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ArrendamientoSoftware.Web.Services
{
    public interface IRolesService
    {
        public Task<Response<ArrendamientoSoftwareRole>> CreateAsync(ArrendamientoSoftwareRoleDTO dto);

        public Task<Response<object>> DeleteAsync(int id);

        public Task<Response<ArrendamientoSoftwareRole>> EditAsync(ArrendamientoSoftwareRoleDTO dto);

        public Task<Response<PaginationResponse<ArrendamientoSoftwareRole>>> GetListAsync(PaginationRequest request);

        public Task<Response<ArrendamientoSoftwareRoleDTO>> GetOneAsync(int id);

        public Task<Response<IEnumerable<Permission>>> GetPermissionsAsync();

        public Task<Response<IEnumerable<PermissionForDTO>>> GetPermissionsByRoleAsync(int id);

        public Task<Response<IEnumerable<Propiedades>>> GetPropiedadesAsync();

    }

    public class RolesService : IRolesService
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public RolesService(DataContext context, IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        public async Task<Response<ArrendamientoSoftwareRole>> CreateAsync(ArrendamientoSoftwareRoleDTO dto)
        {
            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Creación de Rol
                    ArrendamientoSoftwareRole model = _converterHelper.ToRole(dto);
                    EntityEntry<ArrendamientoSoftwareRole> modelStored = await _context.ArrendamientoSoftwareRoles.AddAsync(model);

                    await _context.SaveChangesAsync();

                    // Inserción de permisos
                    int roleId = modelStored.Entity.Id;

                    List<int> permissionIds = new List<int>();

                    if (!string.IsNullOrWhiteSpace(dto.PermissionIds))
                    {
                        permissionIds = JsonConvert.DeserializeObject<List<int>>(dto.PermissionIds);
                    }

                    foreach (int permissionId in permissionIds)
                    {
                        RolePermission rolePermission = new RolePermission
                        {
                            RoleId = roleId,
                            PermissionId = permissionId
                        };

                        await _context.RolePermissions.AddAsync(rolePermission);
                    }

                    // Inserción de secciones
                    //List<int> sectionIds = new List<int>();

                    //if (!string.IsNullOrWhiteSpace(dto.SectionIds))
                    //{
                    //    sectionIds = JsonConvert.DeserializeObject<List<int>>(dto.SectionIds);
                    //}

                    //foreach (int sectionId in sectionIds)
                    //{
                    //    RoleSection roleSection = new RoleSection
                    //    {
                    //        RoleId = roleId,
                    //        SectionId = sectionId
                    //    };

                    //    _context.RoleSections.Add(roleSection);
                    //}

                    await _context.SaveChangesAsync();
                    transaction.Commit();

                    return ResponseHelper<ArrendamientoSoftwareRole>.MakeResponseSuccess(model, "Rol creado con éxito");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return ResponseHelper<ArrendamientoSoftwareRole>.MakeResponseFail(ex);
                }
            }
        }

        public async Task<Response<object>> DeleteAsync(int id)
        {
            try
            {
                Response<ArrendamientoSoftwareRole> roleResponse = await GetOneModelAsync(id);

                if (!roleResponse.IsSuccess)
                {
                    return ResponseHelper<object>.MakeResponseFail(roleResponse.Message);
                }

                if (roleResponse.Result.Name == Env.SUPER_ADMIN_ROLE_NAME)
                {
                    return ResponseHelper<object>.MakeResponseFail($"El rol {Env.SUPER_ADMIN_ROLE_NAME} no puede ser eliminado");
                }

                if (roleResponse.Result.Usuarios.Count() > 0)
                {
                    return ResponseHelper<object>.MakeResponseFail($"El rol no puede ser eliminado debido a que tiene usuarios relacionados");
                }

                _context.ArrendamientoSoftwareRoles.Remove(roleResponse.Result);
                await _context.SaveChangesAsync();

                return ResponseHelper<object>.MakeResponseSuccess("Rol eliminado con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<object>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<ArrendamientoSoftwareRole>> EditAsync(ArrendamientoSoftwareRoleDTO dto)
        {
            try
            {
                if (dto.Name == Env.SUPER_ADMIN_ROLE_NAME)
                {
                    return ResponseHelper<ArrendamientoSoftwareRole>.MakeResponseFail($"El Rol '{Env.SUPER_ADMIN_ROLE_NAME}' no puede ser editado");
                }

                List<int> permissionIds = new List<int>();

                if (!string.IsNullOrEmpty(dto.PermissionIds))
                {
                    permissionIds = JsonConvert.DeserializeObject<List<int>>(dto.PermissionIds);
                }

                // Eliminación de antiguos permisos
                List<RolePermission> oldRolePermissions = await _context.RolePermissions.Where(rs => rs.RoleId == dto.Id).ToListAsync();
                _context.RolePermissions.RemoveRange(oldRolePermissions);

                // Inserción de nuevos permisos
                foreach (int permissionId in permissionIds)
                {
                    RolePermission rolePermission = new RolePermission
                    {
                        RoleId = dto.Id,
                        PermissionId = permissionId
                    };

                    _context.RolePermissions.Add(rolePermission);
                }

                // Secciones
                List<int> sectionIds = new List<int>();

                if (!string.IsNullOrEmpty(dto.PropiedadesIds))
                {
                    sectionIds = JsonConvert.DeserializeObject<List<int>>(dto.PropiedadesIds);
                }

                // Eliminación de antiguas secciones
                //List<RoleSection> oldRoleSections = await _context.RoleSections.Where(rs => rs.RoleId == dto.Id).ToListAsync();
                //_context.RoleSections.RemoveRange(oldRoleSections);

                // Inserción de nuevas secciones
                //foreach (int sectionId in sectionIds)
                //{
                //    RoleSection roleSection = new RoleSection
                //    {
                //        RoleId = dto.Id,
                //        SectionId = sectionId
                //    };

                //    _context.RoleSections.Add(roleSection);
                //}

                // Actualización de rol
                ArrendamientoSoftwareRole model = _converterHelper.ToRole(dto);
                _context.ArrendamientoSoftwareRoles.Update(model);

                await _context.SaveChangesAsync();

                return ResponseHelper<ArrendamientoSoftwareRole>.MakeResponseSuccess(model, "Rol editado con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<ArrendamientoSoftwareRole>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<PaginationResponse<ArrendamientoSoftwareRole>>> GetListAsync(PaginationRequest request)
        {
            try
            {
                IQueryable<ArrendamientoSoftwareRole> queryable = _context.ArrendamientoSoftwareRoles.AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    queryable = queryable.Where(s => s.Name.ToLower().Contains(request.Filter.ToLower()));
                }

                PagedList<ArrendamientoSoftwareRole> list = await PagedList<ArrendamientoSoftwareRole>.ToPagedListAsync(queryable, request);

                PaginationResponse<ArrendamientoSoftwareRole> result = new PaginationResponse<ArrendamientoSoftwareRole>
                {
                    List = list,
                    TotalCount = list.TotalCount,
                    RecordsPerPage = list.RecordsPerPage,
                    CurrentPage = list.CurrentPage,
                    TotalPages = list.TotalPages,
                    Filter = request.Filter,
                };

                return ResponseHelper<PaginationResponse<ArrendamientoSoftwareRole>>.MakeResponseSuccess(result, "Roles obtenidas con éxito");
            }
            catch (Exception ex)
            {
                return ResponseHelper<PaginationResponse<ArrendamientoSoftwareRole>>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<ArrendamientoSoftwareRoleDTO>> GetOneAsync(int id)
        {
            try
            {
                ArrendamientoSoftwareRole? arrendamientoSoftwareRole = await _context.ArrendamientoSoftwareRoles.FirstOrDefaultAsync(r => r.Id == id);

                if (arrendamientoSoftwareRole is null)
                {
                    return ResponseHelper<ArrendamientoSoftwareRoleDTO>.MakeResponseFail($"El Rol con id '{id}' no existe.");
                }

                return ResponseHelper<ArrendamientoSoftwareRoleDTO>.MakeResponseSuccess(await _converterHelper.ToRoleDTOAsync(arrendamientoSoftwareRole));
            }
            catch (Exception ex)
            {
                return ResponseHelper<ArrendamientoSoftwareRoleDTO>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<IEnumerable<Permission>>> GetPermissionsAsync()
        {
            try
            {
                IEnumerable<Permission> permissions = await _context.Permissions.ToListAsync();

                return ResponseHelper<IEnumerable<Permission>>.MakeResponseSuccess(permissions);
            }
            catch (Exception ex)
            {
                return ResponseHelper<IEnumerable<Permission>>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<IEnumerable<PermissionForDTO>>> GetPermissionsByRoleAsync(int id)
        {
            try
            {
                Response<ArrendamientoSoftwareRoleDTO> response = await GetOneAsync(id);

                if (!response.IsSuccess)
                {
                    return ResponseHelper<IEnumerable<PermissionForDTO>>.MakeResponseSuccess(null, response.Message);
                }

                List<PermissionForDTO> permissions = response.Result.Permissions;

                return ResponseHelper<IEnumerable<PermissionForDTO>>.MakeResponseSuccess(permissions);
            }
            catch (Exception ex)
            {
                return ResponseHelper<IEnumerable<PermissionForDTO>>.MakeResponseFail(ex);
            }
        }

        public async Task<Response<IEnumerable<Propiedades>>> GetPropiedadesAsync()
        {
            try
            {
                IEnumerable<Propiedades> propiedades = await _context.Propiedades.Where(s => !s.IsHidden)
                                                                       .ToListAsync();

                return ResponseHelper<IEnumerable<Propiedades>>.MakeResponseSuccess(propiedades);
            }
            catch (Exception ex)
            {
                return ResponseHelper<IEnumerable<Propiedades>>.MakeResponseFail(ex);
            }
        }

        private async Task<Response<ArrendamientoSoftwareRole>> GetOneModelAsync(int id)
        {
            try
            {
                ArrendamientoSoftwareRole? role = await _context.ArrendamientoSoftwareRoles.Include(r => r.Usuarios)
                                                                       .FirstOrDefaultAsync(r => r.Id == id);

                if (role is null)
                {
                    return ResponseHelper<ArrendamientoSoftwareRole>.MakeResponseFail($"El Rol con id '{id}' no existe");
                }

                return ResponseHelper<ArrendamientoSoftwareRole>.MakeResponseSuccess(role);
            }
            catch (Exception ex)
            {
                return ResponseHelper<ArrendamientoSoftwareRole>.MakeResponseFail(ex);
            }
        }
    }
}
