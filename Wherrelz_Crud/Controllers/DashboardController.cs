using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wherrelz_Crud.Data;
using Wherrelz_Crud.ViewModels;

namespace Wherrelz_Crud.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            var totalEntries = await _context.Entries.CountAsync();
            var totalCredit = await _context.Entries.SumAsync(x => x.Credit);
            var totalDebit = await _context.Entries.SumAsync(x => x.Debit);
            var highestCredit = await _context.Entries.MaxAsync(x => x.Credit);
            var highestDebit = await _context.Entries.MaxAsync(x => x.Debit);
            var model = new DashboardVM
            {
                TotalEntries = totalEntries,
                TotalCredit = totalCredit,
                TotalDebit = totalDebit,
                Balance = totalCredit - totalDebit,
                HighestCredit = highestCredit,
                HighestDebit = highestDebit
            };
            return View(model);
        }
    }
}
