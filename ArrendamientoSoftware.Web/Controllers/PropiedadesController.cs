﻿using ArrendamientoSoftware.Web.Core;
using ArrendamientoSoftware.Web.Data.Entities;
using ArrendamientoSoftware.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArrendamientoSoftware.Web.Controllers
{
    public class PropiedadesController : Controller
    {
        private readonly IPropiedadesService _propiedadesService;

        public PropiedadesController(IPropiedadesService propiedadesService)  // Inyección del servicio de propiedades.
        {
            _propiedadesService = propiedadesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Response<List<Propiedades>> response = await _propiedadesService.GetListAsync();
            return View(response.Result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Propiedades propiedades)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(propiedades);
                }

                Response<Propiedades> response = await _propiedadesService.CreateAsync(propiedades);

                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

                // TODO: Mostrar mensaje de error
                return View(response);
            }
            catch (Exception ex)
            {
                // TODO: Manejar el error adecuadamente
                return View(propiedades);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            Response<Propiedades> response = await _propiedadesService.GetOneAsync(id);

            if (response.IsSuccess)
            {
                return View(response.Result);
            }

            // TODO: Mensaje de error
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Propiedades propiedades)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // TODO: Mensaje de error
                    return View(propiedades);
                }

                Response<Propiedades> response = await _propiedadesService.EditAsync(propiedades);

                if (response.IsSuccess)
                {
                    // TODO: Mensaje de éxito
                    return RedirectToAction(nameof(Index));
                }

                // TODO: Mostrar mensaje de error
                return View(response);
            }
            catch (Exception ex)
            {
                // TODO: Mensaje de error
                return View(propiedades);
            }
        }
    }
}



