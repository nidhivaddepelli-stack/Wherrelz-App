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
    public class EntryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EntryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var entries = await _context.Entries.ToListAsync();

            return View(entries);
        }
        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            if (id != null) 
            {
                var record = await _context.Entries.Where(o => o.Id == id).FirstOrDefaultAsync();
                if (record == null) 
                {
                    return RedirectToAction("Index");
                }
                var model = new EntryModel()
                {
                    Id = record.Id,
                    Account = record.Account,
                    Narration = record.Narration,
                    Currency = record.Currency,
                    Credit = record.Credit,
                    Debit = record.Debit
                };

                return View(model);
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(EntryVM entryModel)
        {
                if (entryModel.Id > 0)
                {
                    var record = await _context.Entries.FirstOrDefaultAsync(x => x.Id == entryModel.Id);

                    if (record == null) return RedirectToAction("Index");

                    // Store old values before changing the record
                    var oldAccount = record.Account;
                    var oldNarration = record.Narration;
                    var oldCurrency = record.Currency;
                    var oldCredit = record.Credit;
                    var oldDebit = record.Debit;

                //Updated entry
                var oldRecord = record; 
                    record.Account = entryModel.Account;
                    record.Narration = entryModel.Narration;
                    record.Currency = entryModel.Currency;
                    record.Credit = entryModel.Credit;
                    record.Debit = entryModel.Debit;

                    TempData["SuccessMessage"] = "Entry updated successfully.";

                    if (oldAccount != entryModel.Account)
                        AddAudit("Account", oldAccount, entryModel.Account);

                    if (oldNarration != entryModel.Narration)
                        AddAudit("Narration", oldNarration, entryModel.Narration);

                    if (oldCurrency != entryModel.Currency)
                        AddAudit("Currency", oldCurrency, entryModel.Currency);

                    if (oldCredit != entryModel.Credit)
                        AddAudit(
                            "Credit",
                            oldCredit.ToString(),
                            entryModel.Credit.ToString());

                    if (oldDebit != entryModel.Debit)
                        AddAudit(
                            "Debit",
                            oldDebit.ToString(),
                            entryModel.Debit.ToString());
                    await _context.SaveChangesAsync();
                }
                else 
                {
                    var model = new EntryModel()
                    {
                        Account = entryModel.Account,
                        Narration = entryModel.Narration,
                        Currency = entryModel.Currency,
                        Credit = entryModel.Credit,
                        Debit = entryModel.Debit
                    };

                    _context.Entries.Add(model);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Entry added successfully.";
                }
                
                return RedirectToAction("Index");
        }
        private void AddAudit(string field, string oldValue, string newValue)
        {
            _context.Audits.Add(new AuditModel
            {
                Table = "Entries",
                Field = field,
                OldValue = oldValue,
                NewValue = newValue,
                ChangedBy = "Admin",
                ChangedAt = DateTime.UtcNow
            });

        }
    }

}
