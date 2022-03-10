using eRestoran.Areas.Admin.ViewModels.Skladiste;
using eRestoran.Data;
using eRestoran.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eRestoran.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SkladisteController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<OnlineGost> _userManager;
      
        public SkladisteController(ApplicationDbContext context, UserManager<OnlineGost> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

      
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PregledStanja()
        {
            var model = new StavkaUlazaVM
            {
                Rows = _context.StavkaUlaza.Select(i => new StavkaUlazaVM.Row()
                {
                   Id=i.Id,
                   Kolicina=i.Kolicina,
                   MeniID=i.MeniID,
                   NazivJela=i.Meni.Naziv
                }).ToList()
            };
            return View(model);
        }
        public IActionResult UlazUSkladiste()
        {
            var model = new UlazUSkladisteVM
            {
                Rows = _context.UlazUSkladiste.Select(i => new UlazUSkladisteVM.Row()
                {
                    Id = i.Id,
                  DatumPrijema=i.DatumPrijema,
                  ImeDobavljaca=i.ImeDobavljaca,
                  KolicinaUlaza=i.KolicinaUlaza,
                  Naziv=i.Naziv,
                  UposlenikID=i.UposlenikID
                }).ToList()
            };
            return View(model);
        }
        public IActionResult NaruciStavku()
        {
            var model = new SkladisteNaruciVM();
            return View(model);
        }
        public IActionResult Naruci(SkladisteNaruciVM model)
        {
            var naruceno = new SkladisteNarucenoVM
            {
                Dobavljac = model.Dobavljac,
                Kolicina = model.Kolicina,
                Stavka = model.Stavka

            };
            if(naruceno.Kolicina<0 || naruceno.Dobavljac==null || naruceno.Stavka==null)
            {
                //neispravna narudzba
                return RedirectToAction("NaruciStavku");
            }
            return RedirectToAction("Uspjesno", naruceno);
        }
        public IActionResult Uspjesno(SkladisteNarucenoVM model)
        {
            return View(model);
        }
    }
}
