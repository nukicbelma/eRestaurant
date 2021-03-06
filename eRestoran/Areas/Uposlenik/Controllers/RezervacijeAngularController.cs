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
   // [Authorize(Roles = "Uposlenik")]
    public class RezervacijeAngularController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<OnlineGost> _userManager;
        private readonly SignInManager<OnlineGost> _signInManager;
        public RezervacijeAngularController(ApplicationDbContext context, UserManager<OnlineGost> userManager, SignInManager<OnlineGost> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        // GET: Rezervacije
        public async Task<IActionResult> Index(int? page)
        {
            var rezervacije = _context.RezervacijaStolova;
            var list = await rezervacije.ToListAsync();
            return Ok(Json(list));

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
        public IActionResult EditAngular([FromBody] RezervacijeAngularVM.Row model)
        {

            var rezervacija = _context.RezervacijaStolova.Find(model.id);

            rezervacija.BrojOsoba = model.brojOsoba;
            _context.SaveChanges();
            return Ok();
        }

        [Authorize(Roles = "Uposlenik")]

        // POST: Rezervacije/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RezervacijaStolova rezervacijaStolova)
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
                var rezervacije = _context.RezervacijaStolova;
                var list = await rezervacije.ToListAsync();
                return Ok(Json(list));
            }
            return Ok(); 
        }
 
        private bool RezervacijaStolovaExists(int id)
        {
            return _context.RezervacijaStolova.Any(e => e.Id == id);
        }
    }
}
