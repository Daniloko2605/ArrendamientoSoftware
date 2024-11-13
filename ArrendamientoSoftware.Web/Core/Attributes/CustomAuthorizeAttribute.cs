using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ArrendamientoSoftware.Web.Services;

namespace ArrendamientoSoftware.Web.Core.Attributes
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute(string permission, string module) : base(typeof(CustomAuthorizeFilter))
        {
            Arguments = [permission, module];
        }
    }

    public class CustomAuthorizeFilter : IAsyncAuthorizationFilter
    {
        private readonly string _permission;
        private readonly string _module;
        private readonly IUsuariosService _usuariosService;

        public CustomAuthorizeFilter(string permission, string module, IUsuariosService usuarioService)
        {
            _permission = permission;
            _module = module;
            _usuariosService = usuarioService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            bool isAuthorized = await _usuariosService.CurrentUserIsAuthorizedAsync(_permission, _module);

            if (!isAuthorized)
            {
                // Rechazar el acceso si el usuario no tiene el rol requerido.
                context.Result = new ForbidResult();
            }
        }
    }
}