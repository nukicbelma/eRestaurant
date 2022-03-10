using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eRestoran.Data;
using eRestoran.Models;
using Microsoft.AspNetCore.Authorization;
using X.PagedList;
using Microsoft.AspNetCore.Identity;


namespace eRestoran.Areas.Uposlenik.Controllers
{
    [Area("Uposlenik")]
    [Authorize(Roles = "Uposlenik")]
    public class ObavijestiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<OnlineGost> _userManager;

        public ObavijestiController(ApplicationDbContext context, UserManager<OnlineGost> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: Uposlenik/Narudzbe
        public async Task<IActionResult> Index(int? page)
        {
            var applicationDbContext = _context.Obavijestis;

            var list = await applicationDbContext.ToListAsync();

            return View(list.ToPagedList(page ?? 1, 10));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obavijest = await _context.Obavijestis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (obavijest == null)
            {
                return NotFound();
            }

            return View(obavijest);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var obavijest = await _context.Obavijestis.FindAsync(id);
            _context.Obavijestis.Remove(obavijest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
