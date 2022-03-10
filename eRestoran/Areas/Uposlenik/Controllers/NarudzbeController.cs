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
    public class NarudzbeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<OnlineGost> _userManager;

        public NarudzbeController(ApplicationDbContext context, UserManager<OnlineGost> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Uposlenik/Narudzbe
        public async Task<IActionResult> Index(int? page)
        {
            var applicationDbContext = _context.OnlineNarudzba;

            var list = await applicationDbContext.ToListAsync();

            return View(list.ToPagedList(page ?? 1, 10));
        }

        // GET: Uposlenik/Narudzbe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onlineNarudzba = await _context.OnlineNarudzba
                .Include(o => o.DostavaNalog.Uposlenik)
                .Include(o => o.DostavaNalog.VoziloZaDostavu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (onlineNarudzba == null)
            {
                return NotFound();
            }

            return View(onlineNarudzba);
        }


        // GET: Uposlenik/Narudzbe/Racun/5
        public async Task<IActionResult> Racun(int? id, bool printaj = false)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var narudzba = await _context.OnlineNarudzba
               .Include(o => o.Restoran)
               .Include(o => o.IzdavateljRačuna)
               .Include(o => o.DostavaNalog.Uposlenik)
               .Include(o => o.DostavaNalog.VoziloZaDostavu)
               .FirstOrDefaultAsync(m => m.Id == id);

            IEnumerable<NarudzbaStavka> stavke = await _context.NarudzbaStavka
                .Where(x=>x.OnlineNarudzbaID == id)
                .Include(o => o.DnevnaPonuda)
                .Include(o => o.Meni)
                .ToListAsync();

            ViewBag.Stavke = stavke;
            ViewBag.CijenaStavki = stavke.Sum(x => x.Meni.Cijena * int.Parse(x.Kolicina));
            ViewBag.Popust = ViewBag.CijenaStavki - narudzba.Cijena;
            ViewBag.PrintajRacun = printaj;

            if (narudzba == null)
            {
                return NotFound();
            }

            return View(narudzba);
        }
        public async Task<IActionResult> IzdajRacun(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await _context.OnlineNarudzba
               .FirstOrDefaultAsync(m => m.Id == id);

            narudzba.IzdavateljRačuna = await _userManager.GetUserAsync(User);
            await _context.SaveChangesAsync();

            return RedirectToAction("Racun", new { Id = id, printaj = true });
        }


    }
}
