using eTickets.Context;
using eTickets.Data.Static;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace eTickets.Controllers;

public class AccountController : Controller
{
    //here we are going to inject the user manager, sign in manager and the application db context 
    //we are going to use the user manager to work with the user related data
    private readonly UserManager<ApplicationUser> _userManager;
    //we are going to use the sign in manager to check if the user is signed in 
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly AppDbContext _context;
    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    public async Task<IActionResult> Users()
    {
        var users = await _context.Users.ToListAsync();

        return View(users);
    }

    public IActionResult Login()
    {
        var response = new LoginVM();

        return View(response);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {
        if(!ModelState.IsValid)
            return View(loginVM);

        var userExists = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

        if(userExists != null)
        {
            var passwordCheck = await _userManager.CheckPasswordAsync(userExists,loginVM.Password);
            if (passwordCheck)
            {
                var result = await _signInManager.PasswordSignInAsync(userExists, loginVM.Password, false, false);
                if(result.Succeeded)
                {
                    return RedirectToAction("Movie", "Movie");
                }
            }

            TempData["Error"] = "Wrong credentials! Please try again!";
            return View(loginVM);
        }

        TempData["Error"] = "Wrong credentials! Please try again!";
        return View(loginVM);
    }

    public IActionResult Register()
    {
        var response = new RegisterVM();

        return View(response);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM registerVM)
    {
        if (!ModelState.IsValid)
            return View(registerVM);

        var userExists = await _userManager.FindByEmailAsync(registerVM.EmailAddress);

        if(userExists != null)
        {
            TempData["Error"] = "This email address is already in use!";
            return View(registerVM);
        }

        var newUser = new ApplicationUser()
        {
            FullName = registerVM.FullName,
            Email = registerVM.EmailAddress,
            UserName = registerVM.EmailAddress
        };

        var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

        if(newUserResponse.Succeeded)
            await _userManager.AddToRoleAsync(newUser, UserRoles.User);

        return View("RegisterCompleted");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Movie", "Movie");
    }

}
