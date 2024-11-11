using ArrendamientoSoftware.Web.Helpers;
using ArrendamientoSoftware.Web.Data.Entities;
using ArrendamientoSoftware.Web.DTOs;
using System.Reflection.Metadata;

namespace ArrendamientoSoftware.Web.Helpers
{
    public interface IConverterHelper
    {
        public Usuarios ToUser(UsuariosDTO dto);
        public Task<UsuariosDTO> ToUserDTOAsync(Usuarios usuario, bool isNew = true);
    }

    public class ConverterHelper : IConverterHelper
    {
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(ICombosHelper combosHelper)
        {
            _combosHelper = combosHelper;
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
                Telefono = usuario.PhoneNumber
            };
        }
    }
}
