using ArrendamientoSoftware.Web.Core;
using ArrendamientoSoftware.Web.Data.Entities;
using ArrendamientoSoftware.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArrendamientoSoftware.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersServices _userService;

        public UsersController(IUsersServices userService)  // Inyección del servicio de usuarios.
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Response<List<User>> response = await _userService.GetListAsync();
            return View(response.Result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(user);
                }

                Response<User> response = await _userService.CreateAsync(user);

                if (response.IsSucess)
                {
                    return RedirectToAction(nameof(Index));
                }

                // TODO: Mostrar mensaje de error
                return View(response);
            }
            catch (Exception ex)
            {
                // TODO: Manejar el error adecuadamente
                return View(user);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            Response<User> response = await _userService.GetOneAsync(id);

            if (response.IsSucess)
            {
                return View(response.Result);
            }

            // TODO: Mensaje de error
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // TODO: Mensaje de error
                    return View(user);
                }

                Response<User> response = await _userService.EditAsync(user);

                if (response.IsSucess)
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
                return View(user);
            }
        }
    }
}
