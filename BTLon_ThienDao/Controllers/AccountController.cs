using BTLon_ThienDao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace BTLon_ThienDao.Controllers
{
    public class AccountController : BaseController
    {
        private readonly QLBTLViecLamDBContext _context;

        public AccountController(QLBTLViecLamDBContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("AccID, Username, Password")] Account model)
        {
            if (ModelState.IsValid)
            {
                var loginUser = await _context.Accounts.FirstOrDefaultAsync(m => m.Username == model.Username);
                if (loginUser == null)
                {
                    ModelState.AddModelError("", "Dang nhap that bai");
                    return View(model);
                }
                else
                {
                    SHA256 hashMenthod = SHA256.Create();
                    if (Util.Cryptography.VerifyHash(hashMenthod, model.Password, loginUser.Password))
                    {
                        CurrentUser = loginUser.Username;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Dang nhap that bai");
                        return View(model);
                    }
                }
            }
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Username,Password")] Account model)
        {
            if (ModelState.IsValid)
            {
                SHA256 hashMenthod = SHA256.Create();
                model.Password = Util.Cryptography.GetHash(hashMenthod, model.Password);

                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }
            return View(model);
        }
        public IActionResult Logout()
        {
            CurrentUser = "";
            return RedirectToAction("Login");
        }
    }
}
