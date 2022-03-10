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
using Microsoft.AspNetCore.Identity;
using X.PagedList.Mvc.Core;
using X.PagedList;
using eRestoran.Areas.Uposlenik.ViewModels;

namespace eRestoran.Areas.Uposlenik.Controllers
{
    [Area("Uposlenik")]
    [Authorize(Roles = "Uposlenik")]
    public class RezervacijeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<OnlineGost> _userManager;
        private readonly SignInManager<OnlineGost> _signInManager;
        public RezervacijeController(ApplicationDbContext context, UserManager<OnlineGost> userManager, SignInManager<OnlineGost> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        // GET: Rezervacije
        public async Task<IActionResult> Index(int? page)
        {
            List<RezervacijaStolova> rezervacije = await _context.RezervacijaStolova.Select(x => new RezervacijaStolova
            {
                Id = x.Id,
                UserName = x.UserName,
                BrojOsoba = x.BrojOsoba,
                BrojTelefona = x.BrojTelefona,
                VrijemeRezervacije = x.VrijemeRezervacije
            })
                .OrderByDescending(x=>x.VrijemeRezervacije)
                .ToListAsync();
            return View(rezervacije.ToPagedList(page ?? 1, 10));
            
        }
        [Authorize(Roles = "Uposlenik")]

        // GET: Rezervacije/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rezervacije/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BrojOsoba,VrijemeRezervacije,BrojTelefona,UserName")] RezervacijaStolova rezervacijaStolova)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rezervacijaStolova);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Uposlenik")]

        // GET: Rezervacije/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacijaStolova = await _context.RezervacijaStolova.FindAsync(id);
            if (rezervacijaStolova == null)
            {
                return NotFound();
            }
            return View(rezervacijaStolova);
        }
        [Authorize(Roles = "Uposlenik")]

        // POST: Rezervacije/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BrojOsoba,VrijemeRezervacije,BrojTelefona,UserName")] RezervacijaStolova rezervacijaStolova)
        {
            if (id != rezervacijaStolova.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezervacijaStolova);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezervacijaStolovaExists(rezervacijaStolova.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(rezervacijaStolova);
        }
        [Authorize(Roles = "Uposlenik")]

        // GET: Rezervacije/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervacijaStolova = await _context.RezervacijaStolova
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rezervacijaStolova == null)
            {
                return NotFound();
            }

            return View(rezervacijaStolova);
        }
        [Authorize(Roles = "Uposlenik")]

        // POST: Rezervacije/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rezervacijaStolova = await _context.RezervacijaStolova.FindAsync(id);
            _context.RezervacijaStolova.Remove(rezervacijaStolova);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezervacijaStolovaExists(int id)
        {
            return _context.RezervacijaStolova.Any(e => e.Id == id);
        }
    }
}
