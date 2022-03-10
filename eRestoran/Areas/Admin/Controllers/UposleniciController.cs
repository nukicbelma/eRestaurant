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
using eRestoran.Areas.Admin.ViewModels.Uposlenici;

namespace eRestoran.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] 
    public class UposleniciController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<OnlineGost> _userManager;
        public UposleniciController(ApplicationDbContext context, UserManager<OnlineGost> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        
        public async Task <IActionResult> Index (int? page)
        {
            var uposlenici = _context.Uposleniks;
            var list = await uposlenici.ToListAsync();
           // return Ok(Json(list)); // angular
            return View(list.ToPagedList(page ?? 1, 10));
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var uposlenici = _context.Uposleniks.FirstOrDefault(i => i.Id == id);
            if(uposlenici==null)
            {
                return NotFound();
            }
            return View(uposlenici);
        }

        public async Task<IActionResult> Save(UposleniciCreateVM model)
        {
            var restoran = _context.Restoran.Where(i => i.Id == 1).SingleOrDefault();
            var uposlenik = new eRestoran.Models.Uposlenik
            {
                Id = model.Id,
                Ime = model.Ime,
                Prezime = model.Prezime,
                RestoranID = restoran.Id,
                Restoran = restoran,
                Titula = model.Titula

            };
            _context.Uposleniks.Add(uposlenik);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Create ()
        {
            return View();
        }
        public async Task<IActionResult> Delete(int? id)
        {
           
            var uposlenik = await _context.Uposleniks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uposlenik == null)
            {
                return NotFound();
            }
            else
            {
                _context.Uposleniks.Remove(uposlenik);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));


        }
   
    }
}
