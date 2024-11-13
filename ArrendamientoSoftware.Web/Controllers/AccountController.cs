using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using ArrendamientoSoftware.Web.DTOs;
using ArrendamientoSoftware.Web.Services;

namespace ArrendamientoSoftware.Web.Controllers
{
	public class AccountController : Controller
	{
		private readonly INotyfService _notifyService;
		private readonly IUsuariosService _usuariosService;

        public AccountController(IUsuariosService usuariosService, INotyfService notifyService)
        {
            _usuariosService = usuariosService;
            _notifyService = notifyService;
        }

        [HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginDTO dto)
		{
			if (ModelState.IsValid) 
			{
                Microsoft.AspNetCore.Identity.SignInResult result = await _usuariosService.LoginAsync(dto);

                if (result.Succeeded)
                {
					return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos");
                _notifyService.Error("Email o contraseña incorrectos");

				return View(dto);
            }

			return View(dto);
		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await _usuariosService.LogoutAsync();
			return RedirectToAction(nameof(Login));
		}

		[HttpGet]
		public IActionResult NotAuthorized()
		{
			return View();
		}

    }
}
