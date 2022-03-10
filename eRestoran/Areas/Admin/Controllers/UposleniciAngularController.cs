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
    //[Authorize(Roles = "Admin")] //zakomentarisano zbog angulara
    public class UposleniciAngularController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<OnlineGost> _userManager;
        public UposleniciAngularController(ApplicationDbContext context, UserManager<OnlineGost> userManager)
        {
            _context = context;
            _userManager = userManager;

        }


        public async Task<IActionResult> Index(int? page)
        {
            var uposlenici = _context.Uposleniks;
            var list = await uposlenici.ToListAsync();
            return Ok(Json(list)); // angular
                                   // return View(list.ToPagedList(page ?? 1, 10));
        }

     
    
        public IActionResult SnimiAngular([FromBody] UposleniciAngularVM.Row model)
        {
           
            var uposlenikId = _context.Uposleniks.Where(x => x.Ime==model.ime).Select(x => x.Id).SingleOrDefault();
            var UPOSLENIK = _context.Uposleniks.Include(x=>x.Restoran).Single(x=>x.Id==uposlenikId);
            UPOSLENIK.Ime = model.ime;
            UPOSLENIK.Prezime = model.prezime;
            _context.SaveChanges();
            return Ok();
        }
    }
}
