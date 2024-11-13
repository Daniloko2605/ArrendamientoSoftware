using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using ArrendamientoSoftware.Web.Core;
using ArrendamientoSoftware.Web.Core.Pagination;
using ArrendamientoSoftware.Web.Data.Entities;
using ArrendamientoSoftware.Web.DTOs;
using ArrendamientoSoftware.Web.Helpers;
using ArrendamientoSoftware.Web.Services;

namespace ArrendamientoSoftware.Web.Controllers
{
    public class TipoInmuebleController : Controller
    {
        private readonly ITipoInmuebleService _tipoInmuebleService;
        private readonly ICombosHelper _combosHelper;
        private readonly INotyfService _notifyService;
        private readonly IConverterHelper _converterHelper;

        public TipoInmuebleController(ITipoInmuebleService tipoInmuebleService, ICombosHelper combosHelper, INotyfService notifyService, IConverterHelper converterHelper)
        {
            _tipoInmuebleService = tipoInmuebleService;
            _combosHelper = combosHelper;
            _notifyService = notifyService;
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

            Response<PaginationResponse<TipoInmueble>> response = await _tipoInmuebleService.GetListAsync(request);
            return View(response.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TipoInmuebleDTO dto = new TipoInmuebleDTO
            {
                Propiedades = await _combosHelper.GetComboPropiedades(),
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TipoInmuebleDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notifyService.Error("Debe ajustar los errores de validación");
                dto.Propiedades = await _combosHelper.GetComboPropiedades();
                return View(dto);
            }

            Response<TipoInmueble> response = await _tipoInmuebleService.CreateAsync(dto);

            if (!response.IsSuccess)
            {
                _notifyService.Error(response.Message);
                dto.Propiedades = await _combosHelper.GetComboPropiedades();
                return View(dto);
            }

            _notifyService.Success(response.Message);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            Response<TipoInmueble> response = await _tipoInmuebleService.GetOneAsync(id);

            if (response.IsSuccess)
            {
                TipoInmuebleDTO dto = await _converterHelper.ToTipoInmuebleDTO(response.Result);

                return View(dto);
            }

            _notifyService.Error(response.Message);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TipoInmuebleDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _notifyService.Error("Debe ajustar los errores de validación");
                    return View(dto);
                }

                Response<TipoInmueble> response = await _tipoInmuebleService.EditAsync(dto);

                if (response.IsSuccess)
                {
                    _notifyService.Success(response.Message);
                    return RedirectToAction(nameof(Index));
                }

                _notifyService.Error(response.Message);
                dto.Propiedades = await _combosHelper.GetComboPropiedades();
                return View(dto);
            }
            catch (Exception ex)
            {
                _notifyService.Error(ex.Message);
                dto.Propiedades = await _combosHelper.GetComboPropiedades();
                return View(dto);
            }
        }
    }
}