using ArrendamientoSoftware.Web.Core.Pagination;
using ArrendamientoSoftware.Web.Helpers;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ArrendamientoSoftware.Web.Core;
using ArrendamientoSoftware.Web.Data.Entities;
using ArrendamientoSoftware.Web.DTOs;
using ArrendamientoSoftware.Web.Services;

namespace ArrendamientoSoftware.Web.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly ICombosHelper _combosHelper;
        private readonly INotyfService _notifyService;
        private readonly IUsuariosService _usuariosService;
        private readonly IConverterHelper _converterHelper;

        public UsuariosController(ICombosHelper combosHelper, INotyfService notifyService, IUsuariosService usuariosService, IConverterHelper converterHelper)
        {
            _combosHelper = combosHelper;
            _notifyService = notifyService;
            _usuariosService = usuariosService;
            _converterHelper = converterHelper;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int? RecordsPerPage,
                                               [FromQuery] int? Page,
                                               [FromQuery] string? Filter)
        {
            PaginationRequest request = new PaginationRequest
            {
                RecordsPerPage = RecordsPerPage ?? 15,
                Page = Page ?? 1,
                Filter = Filter
            };

            Response<PaginationResponse<Usuarios>> response = await _usuariosService.GetListAsync(request);
            return View(response.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            UsuariosDTO dto = new UsuariosDTO
            {
                ArrendamientoSoftwareRoles = await _combosHelper.GetComboArrendamientoSoftwareRolesAsync(),
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsuariosDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Debe ajustar los errores de validación");
                    dto.ArrendamientoSoftwareRoles = await _combosHelper.GetComboArrendamientoSoftwareRolesAsync();
                    return View(dto);
                }

                Response<Usuarios> response = await _usuariosService.CreateAsync(dto);

                if (response.IsSuccess)
                {
                    _notifyService.Success(response.Message);
                    return RedirectToAction(nameof(Index));
                }

                _notifyService.Error(response.Message);
                dto.ArrendamientoSoftwareRoles = await _combosHelper.GetComboArrendamientoSoftwareRolesAsync();
                return View(dto);
            }
            catch (Exception ex)
            {
                dto.ArrendamientoSoftwareRoles = await _combosHelper.GetComboArrendamientoSoftwareRolesAsync();
                return View(dto);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (Guid.Empty.Equals(id))
            {
                return NotFound();
            }

            Usuarios usuarios = await _usuariosService.GetUsuariosAsync(id);

            if (usuarios is null)
            {
                return NotFound();
            }

            UsuariosDTO dto = await _converterHelper.ToUserDTOAsync(usuarios, false);

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsuariosDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notifyService.Error("Debe ajustar los errores de validación");
                dto.ArrendamientoSoftwareRoles = await _combosHelper.GetComboArrendamientoSoftwareRolesAsync();
                return View(dto);
            }

            Response<Usuarios> response = await _usuariosService.UpdateUsuariosAsync(dto);

            if (response.IsSuccess)
            {
                _notifyService.Success(response.Message);
                return RedirectToAction(nameof(Index));
            }

            _notifyService.Error(response.Message);
            dto.ArrendamientoSoftwareRoles = await _combosHelper.GetComboArrendamientoSoftwareRolesAsync();
            return View(dto);
        }

    }
}
