using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wherrelz_Crud.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Wherrelz_Crud.Data;
using Wherrelz_Crud.Models;
using Wherrelz_Crud.ViewModels;

namespace Wherrelz_Crud.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();

            return View(users);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var record = await _context.Users.Where(o => o.Id == id).FirstOrDefaultAsync();
                if (record == null)
                {
                    return RedirectToAction("Index");
                }
                var model = new UserVM()
                {
                    Id = record.Id,
                    LoginId = record.LoginId,
                    FullName = record.FullName,
                    Email = record.Email
                };
                return Json(model);
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult> Create(UserVM userViewmodel)
        {
                if (userViewmodel.HiddenId > 0)
                {
                    var record = await _context.Users.FirstOrDefaultAsync(x => x.Id == userViewmodel.HiddenId);
                    if (record == null) return RedirectToAction("Index");

                // Check if another user already has this LoginId
                var loginIdExists = await _context.Users.AnyAsync(x => x.LoginId == userViewmodel.LoginId && x.Id != userViewmodel.HiddenId);
                if (loginIdExists)
                {
                    TempData["ErrorMessage"] = "Login ID already exists.";
                    return RedirectToAction("Index");
                }

                    record.LoginId = userViewmodel.LoginId;
                    record.FullName = userViewmodel.FullName;
                    record.Email = userViewmodel.Email;
                    
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Entry updated successfully.";
                }
                else
                {
                // Check LoginId before adding
                    var loginIdExists = await _context.Users.AnyAsync(x => x.LoginId == userViewmodel.LoginId);
                    if (loginIdExists)
                    {
                        TempData["ErrorMessage"] = "Login ID already exists.";
                        return RedirectToAction("Index");
                    }
                    var model = new UserModel()
                        {
                            LoginId = userViewmodel.LoginId,
                            FullName = userViewmodel.FullName,
                            Email = userViewmodel.Email,
                            Password = userViewmodel.Password,
                            IsActive = true,
                        };

                    _context.Users.Add(model);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Entry added successfully.";
                }

                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int hiddenid)
        {
            var user = await _context.Users.Where(o => o.Id == hiddenid).FirstOrDefaultAsync();

            if (user == null)
                if (user == null) return RedirectToAction("Index");

            user.IsActive = false;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
