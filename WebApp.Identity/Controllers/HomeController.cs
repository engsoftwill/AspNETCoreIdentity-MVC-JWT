using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Identity.Models;

namespace WebApp.Identity.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly UserManager<MyUser> _userManager;
        public HomeController(UserManager<MyUser> userManager)
        {
            _userManager = userManager;
        }
        

   

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid) // se password e confirme password for validado
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var identity = new ClaimsIdentity("cookies");
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

                    await HttpContext.SignInAsync("cookies", new ClaimsPrincipal(identity));

                    return RedirectToAction("About");
                }
                
                ModelState.AddModelError("", "Usuario ou senha invalidos");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid) // se password e confirme password for validado
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    user = new MyUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = model.UserName
                    };

                    var result = await _userManager.CreateAsync(
                        user, model.Password);
                }
                    return View("Success");
            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> About()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Success()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
