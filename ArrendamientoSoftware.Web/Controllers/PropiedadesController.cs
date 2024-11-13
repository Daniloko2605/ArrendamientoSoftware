using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ArrendamientoSoftware.Web.Core;
using ArrendamientoSoftware.Web.Core.Attributes;
using ArrendamientoSoftware.Web.Core.Pagination;
using ArrendamientoSoftware.Web.Data.Entities;
using ArrendamientoSoftware.Web.Helpers;
using ArrendamientoSoftware.Web.Requests;
using ArrendamientoSoftware.Web.Services;

namespace ArrendamientoSoftware.Web.Controllers
{
    [Authorize]
    public class PropiedadesController : Controller
    {
        private readonly IPropiedadesService _propiedadesService;
        private readonly INotyfService _notifyService;

        public PropiedadesController(IPropiedadesService propiedadesService, INotyfService notifyService)
        {
            _propiedadesService = propiedadesService;
            _notifyService = notifyService;
        }

        [HttpGet]
        [CustomAuthorize(permission: "showPropiedades", module: "Propiedades")]
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

            Response<PaginationResponse<Propiedades>> response = await _propiedadesService.GetListAsync(request);
            return View(response.Result);
        }

        [HttpGet]
        [CustomAuthorize(permission: "createPropiedades", module: "Propiedades")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [CustomAuthorize(permission: "createPropiedades", module: "Propiedades")]
        public async Task<IActionResult> Create(Propiedades propiedades)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Debe ajustar los errores de validación");
                    return View(propiedades);
                }

                Response<Propiedades> response = await _propiedadesService.CreateAsync(propiedades);

                if (response.IsSuccess)
                {
                    _notifyService.Success(response.Message);
                    return RedirectToAction(nameof(Index));
                }

                _notifyService.Error(response.Message);
                return View(response);
            }
            catch (Exception ex)
            {
                return View(propiedades);
            }
        }

        [HttpGet]
        [CustomAuthorize(permission: "editPropiedades", module: "Propiedades")]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            Response<Propiedades> response = await _propiedadesService.GetOneAsync(id);

            if (response.IsSuccess)
            {
                return View(response.Result);
            }

            _notifyService.Error(response.Message);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [CustomAuthorize(permission: "editPropiedades", module: "Propiedades")]
        public async Task<IActionResult> Edit(Propiedades propiedades)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Debe ajustar los errores de validación");
                    return View(propiedades);
                }

                Response<Propiedades> response = await _propiedadesService.EditAsync(propiedades);

                if (response.IsSuccess)
                {
                    _notifyService.Success(response.Message);
                    return RedirectToAction(nameof(Index));
                }

                _notifyService.Error(response.Message);
                return View(response);
            }
            catch (Exception ex)
            {
                _notifyService.Error(ex.Message);
                return View(propiedades);
            }
        }

        [HttpPost]
        [CustomAuthorize(permission: "deletePropiedades", module: "Propiedades")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Response<Propiedades> response = await _propiedadesService.DeleteAsync(id);

            if (response.IsSuccess)
            {
                _notifyService.Success(response.Message);
            }
            else
            {
                _notifyService.Error(response.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [CustomAuthorize(permission: "editPropiedades", module: "Propiedades")]
        public async Task<IActionResult> Toggle(int PropiedadesId, bool Hide)
        {
            TogglePropiedadesStatusRequest request = new TogglePropiedadesStatusRequest
            {
                Hide = Hide,
                PropiedadesId = PropiedadesId
            };

            Response<Propiedades> response = await _propiedadesService.ToggleAsync(request);

            if (response.IsSuccess)
            {
                _notifyService.Success(response.Message);
            }
            else
            {
                _notifyService.Error(response.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}