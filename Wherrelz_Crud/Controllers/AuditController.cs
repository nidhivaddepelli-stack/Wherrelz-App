using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wherrelz_Crud.Data;
using Wherrelz_Crud.Models;

namespace Wherrelz_Crud.Controllers
{
    [Authorize]
    public class AuditController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuditController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var audit = await _context.Audits.ToListAsync();

            return View(audit);
        }

    }
}
