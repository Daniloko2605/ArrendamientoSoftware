using ArrendamientoSoftware.Web.Core;
using ArrendamientoSoftware.Web.Data.Entities;
using ArrendamientoSoftware.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArrendamientoSoftware.Web.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuariosService _usuariosService;

        public UsuariosController(IUsuariosService usuariosService)  // Inyección del servicio de usuarios.
        {
            _usuariosService = usuariosService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Response<List<Usuarios>> response = await _usuariosService.GetListAsync();
            return View(response.Result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Usuarios usuarios)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(usuarios);
                }

                Response<Usuarios> response = await _usuariosService.CreateAsync(usuarios);

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
                return View(usuarios);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            Response<Usuarios> response = await _usuariosService.GetOneAsync(id);

            if (response.IsSuccess)
            {
                return View(response.Result);
            }

            // TODO: Mensaje de error
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Usuarios usuarios)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // TODO: Mensaje de error
                    return View(usuarios);
                }

                Response<Usuarios> response = await _usuariosService.EditAsync(usuarios);

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
                return View(usuarios);
            }
        }
    }
}
