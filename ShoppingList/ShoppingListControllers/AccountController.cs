using AuthApp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Controllers.ViewModels;
using ShoppingList.DataBase;
using ShoppingList.Domain;
using ShoppingList.Domain.Abstractions;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthApp.Controllers
{
	[Route("api/[controller]")]
	public class AccountController : Controller
	{
		private Hasher hasher;
		private DataContext db;
		private IValidationService validationService;
		public AccountController(DataContext context, Hasher hasher, IValidationService validationService)
		{
			db = context;
			this.hasher = hasher;
			this.validationService = validationService;
		}

		[Authorize]
		[HttpPost("password/change")]
		public async Task<IActionResult> ChangePassword([FromBody]RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				User user = await db.Users
					.Include(x => x.Passwords)
					.FirstOrDefaultAsync(u => u.Email == model.Email);
				if (validationService.ChangingPassword(user, model.Password))
				{
					await Authenticate(model.Email);
					return Ok();
				}
			}
			return NotFound();
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody]LoginModel model)
		{
			if (ModelState.IsValid)
			{
				User user = await db.Users
					.Include(x => x.Passwords)
					.FirstOrDefaultAsync(u => u.Email == model.Email);
				if (validationService.LogingIn(user, model.Password))
				{
					await Authenticate(model.Email);
					return Ok();
				}
			}
			return StatusCode(401);
		}

		[HttpPost("registration")]
		public async Task<IActionResult> Register([FromBody]RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
				if (validationService.Registering(user, model.Email, model.Login, model.Password))
				{
					await Authenticate(model.Email); // аутентификация
					return Ok();
				}
			}
			return NotFound();
		}

		private async Task Authenticate(string userName)
		{
			// создаем один claim
			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
			};
			// создаем объект ClaimsIdentity
			ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
			// установка аутентификационных куки
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return Ok();
		}
	}
}