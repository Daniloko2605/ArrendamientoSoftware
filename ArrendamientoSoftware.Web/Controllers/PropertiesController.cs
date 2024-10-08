using ArrendamientoSoftware.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ArrendamientoSoftware.Web.Controllers
{
    public class PropertiesController : Controller
    {
        //private readonly IpropiedadsService _propiedadsService;

        //public PropertiesController(IpropiedadsService propiedadsService) 
        //{
        //    _propiedadsService = propiedadsService;
        //}

        //[HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Properties>propiedades = new List<Properties>
            {
                new Properties
                {
                    Id = 1,
                    Direccion = "Carrera 19 #22-67",
                    Ciudad = "Medellín",
                    CreatedDate = DateTime.Now,
                    Descripcion = "Casa finca en Guayabal",
                    Owner = "Raul Gonzales",
                    IdOwner = 1, 
                    Precio = 500000000, 
                    UpdatedDate = DateTime.Now,

                }
            };
            return View(propiedades);
        }

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(Propiedad propiedad)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return View(propiedad);
        //        }

        //        Response<Propiedad> response = await _propiedadsService.CreateAsync(propiedad);

        //        if (response.IsSucess)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }

        //        //TO DO: Mostrar mensaje de error
        //        return View(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return View(propiedad);
        //    }
        //}

        //[HttpGet]
        //public async Task<IActionResult> Edit([FromRoute] int id)
        //{
        //    Response<Propiedad> response = await _propiedadsService.GetOneAsync(id);

        //    if (response.IsSucess)
        //    {
        //        return View(response.Result);
        //    }

        //    //TODO: Mensaje de error
        //    return RedirectToAction(nameof(Index));
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(Propiedad propiedad)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            //TODO: Mensaje de error
        //            return View(propiedad);
        //        }

        //        Response<Propiedad> response = await _propiedadsService.EditAsync(propiedad);

        //        if (response.IsSucess)
        //        {
        //            //TODO: Mensaje de éxito
        //            return RedirectToAction(nameof(Index));
        //        }

        //        //TODO: Mostrar mensaje de error
        //        return View(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        //TODO: Mensaje de error
        //        return View(propiedad);
        //    }
        //}
    }
}
