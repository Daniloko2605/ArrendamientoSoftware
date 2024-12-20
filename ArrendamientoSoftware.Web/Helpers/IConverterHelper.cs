<<<<<<< HEAD
﻿using Microsoft.EntityFrameworkCore;
using ArrendamientoSoftware.Web.Data;
using ArrendamientoSoftware.Web.Data.Entities;
using ArrendamientoSoftware.Web.DTOs;
using System.Data;
=======
﻿using ArrendamientoSoftware.Web.Helpers;
using ArrendamientoSoftware.Web.Data.Entities;
using ArrendamientoSoftware.Web.DTOs;
using System.Reflection.Metadata;
>>>>>>> 3ea28f371e27d22435e1645cd9a4daf102c15886

namespace ArrendamientoSoftware.Web.Helpers
{
    public interface IConverterHelper
    {
<<<<<<< HEAD
        public TipoInmueble ToTipoInmueble(TipoInmuebleDTO dto);
        public Task<TipoInmuebleDTO> ToTipoInmuebleDTO(TipoInmueble result);
        public ArrendamientoSoftwareRole ToRole(ArrendamientoSoftwareRoleDTO dto);
        public Task<ArrendamientoSoftwareRoleDTO> ToRoleDTOAsync(ArrendamientoSoftwareRole arrendamientoSoftwareRole);
=======
>>>>>>> 3ea28f371e27d22435e1645cd9a4daf102c15886
        public Usuarios ToUser(UsuariosDTO dto);
        public Task<UsuariosDTO> ToUserDTOAsync(Usuarios usuario, bool isNew = true);
    }

    public class ConverterHelper : IConverterHelper
    {
        private readonly ICombosHelper _combosHelper;
<<<<<<< HEAD
        private readonly DataContext _context;

        public ConverterHelper(ICombosHelper combosHelper, DataContext context)
        {
            _combosHelper = combosHelper;
            _context = context;
        }

        public TipoInmueble ToTipoInmueble(TipoInmuebleDTO dto)
        {
            return new TipoInmueble
            {
                Local = dto.Local,
                Oficina = dto.Oficina,
                Bodega = dto.Bodega,
                Casa = dto.Casa,
                Apartamento = dto.Apartamento,
                Finca = dto.Finca,
                Id = dto.Id,
                IsPublished = dto.IsPublished,
                PropiedadId = dto.PropiedadId,
            };
        }

        public async Task<TipoInmuebleDTO> ToTipoInmuebleDTO(TipoInmueble tipoInmueble)
        {
            return new TipoInmuebleDTO
            {
                Local = tipoInmueble.Local,
                Oficina = tipoInmueble.Oficina,
                Bodega = tipoInmueble.Bodega,
                Casa = tipoInmueble.Casa,
                Apartamento = tipoInmueble.Apartamento,
                Finca = tipoInmueble.Finca,
                Id = tipoInmueble.Id,
                IsPublished = tipoInmueble.IsPublished,
                PropiedadId = tipoInmueble.PropiedadId,
                Propiedades= await _combosHelper.GetComboPropiedades()
            };
        }

        public ArrendamientoSoftwareRole ToRole(ArrendamientoSoftwareRoleDTO dto)
        {
            return new ArrendamientoSoftwareRole
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }

        public async Task<ArrendamientoSoftwareRoleDTO> ToRoleDTOAsync(ArrendamientoSoftwareRole role)
        {
            List<PermissionForDTO> permissions = await _context.Permissions.Select(p => new PermissionForDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Module = p.Module,
                Selected = _context.RolePermissions.Any(rp => rp.PermissionId == p.Id && rp.RoleId == role.Id)
            }).ToListAsync();

            return new ArrendamientoSoftwareRoleDTO
            {
                Id = role.Id,
                Name = role.Name,
                Permissions = permissions,
            };
=======

        public ConverterHelper(ICombosHelper combosHelper)
        {
            _combosHelper = combosHelper;
>>>>>>> 3ea28f371e27d22435e1645cd9a4daf102c15886
        }

        public Usuarios ToUser(UsuariosDTO dto)
        {
            return new Usuarios
            {
                Id = dto.Id.ToString(),
                Documento = dto.Documento,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Email = dto.Email,
<<<<<<< HEAD
                UserName = dto.Email,
=======
>>>>>>> 3ea28f371e27d22435e1645cd9a4daf102c15886
                ArrendamientoSoftwareRoleId = dto.ArrendamientoSoftwareRoleId,
                PhoneNumber = dto.Telefono,
            };
        }

        public async Task<UsuariosDTO> ToUserDTOAsync(Usuarios usuario, bool isNew = true)
        {
            return new UsuariosDTO
            {
                Id = isNew ? Guid.NewGuid() : Guid.Parse(usuario.Id),
                Documento = usuario.Documento,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                ArrendamientoSoftwareRoles = await _combosHelper.GetComboArrendamientoSoftwareRolesAsync(),
                ArrendamientoSoftwareRoleId = usuario.ArrendamientoSoftwareRoleId,
<<<<<<< HEAD
                Telefono = usuario.PhoneNumber,
=======
                Telefono = usuario.PhoneNumber
>>>>>>> 3ea28f371e27d22435e1645cd9a4daf102c15886
            };
        }
    }
}
