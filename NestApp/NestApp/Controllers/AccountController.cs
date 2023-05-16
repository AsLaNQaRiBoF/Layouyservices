using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NestApp.Models;
using NestApp.ViewModel.AccVms;

namespace NestApp.Controllers
{
    public class AccountController : Controller


     private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager
    {
        _userManager=userManager
        _signInManager=signInManager
    }


    public IActionResult Register()
    {
        return View();
    }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
        if (!ModelState.isValid) return View();
        AppUser newuser = new AppUser();
        {
            Name=registerVM.Name;
            Surname=registerVM.SurName;
            Email=registerVM.Email;
            UserNAme=registerVM.UserName;
        };
       IdentityResult result= await _userManager.CreateAsync(newuser,registerVM,Password,);
        if (!result.Succeeded) {
            foreach(var item in result.Errors) 
            { 
            ModelState.AddModelError("", item.Description);
            }
            return View(item);
        }
        await _signInManager SignInAsync(newuser ,false);
        return RedirectToAction("index","Home")
        public IActionResult Login()
        {
            return View();
        }
        }

    }

