using ArrendamientoSoftware.Web.Core;
using ArrendamientoSoftware.Web.Data.Entities;
using ArrendamientoSoftware.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArrendamientoSoftware.Web.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly IPropertiesService _propertiesService;

        public PropertiesController(IPropertiesService propertiesService)  //Con esto se inyectó el servicio de propiedades.
        {
            _propertiesService = propertiesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            Response<List<Properties>> response = await _propertiesService.GetListAsync();
            return View(response.Result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Properties properties)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(properties);
                }

                Response<Properties> response = await _propertiesService.CreateAsync(properties);

                if (response.IsSucess)
                {
                    return RedirectToAction(nameof(Index));
                }

                //TO DO: Mostrar mensaje de error
                return View(response);
            }
            catch (Exception ex)
            {
                return View(properties);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            Response<Properties> response = await _propertiesService.GetOneAsync(id);

            if (response.IsSucess)
            {
                return View(response.Result);
            }

            //TODO: Mensaje de error
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Properties properties)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    //TODO: Mensaje de error
                    return View(properties);
                }

                Response<Properties> response = await _propertiesService.EditAsync(properties);

                if (response.IsSucess)
                {
                    //TODO: Mensaje de éxito
                    return RedirectToAction(nameof(Index));
                }

                //TODO: Mostrar mensaje de error
                return View(response);
            }
            catch (Exception ex)
            {
                //TODO: Mensaje de error
                return View(properties);
            }
        }
    }
}

