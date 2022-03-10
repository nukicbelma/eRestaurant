using eRestoran.Data;
using eRestoran.Models;
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
using eRestoran.Areas.Admin.ViewModels.Rezervacije;

namespace eRestoran.Areas.Admin.Controllers
{
    public class RezervacijeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<OnlineGost> _userManager;
        private readonly SignInManager<OnlineGost> _signInManager;
        public RezervacijeController(ApplicationDbContext context, UserManager<OnlineGost> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            RezervacijeIndexVM model = new RezervacijeIndexVM
            {
                Rows = _context.RezervacijaStolova.Select(i => new RezervacijeIndexVM.Row
                {
                    RezervacijaId=i.Id,
                    BrojOsoba=i.BrojOsoba,
                    BrojTelefona=i.BrojTelefona,
                    UserName=i.UserName,
                    VrijemeRezervacije=i.VrijemeRezervacije
                }).ToList()
            };
            return View(model);

        }
        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var rezervacija = _context.RezervacijaStolova.Where(i => i.Id == id).SingleOrDefault();
            var model = new RezervacijeEditVM
            {
                BrojOsoba = rezervacija.BrojOsoba,
                BrojTelefona = rezervacija.BrojTelefona,
                Id = rezervacija.Id,
                UserName = rezervacija.UserName,
                VrijemeRezervacije = rezervacija.VrijemeRezervacije
            };
            

            return View(model);
        }
        [Area("Admin")]
        [Authorize(Roles = "Admin")]
       
        public IActionResult Spasi(RezervacijeEditVM model)
        {

            var rezervacijaStolova = _context.RezervacijaStolova.Find(model.Id);
            rezervacijaStolova.UserName = model.UserName;
            rezervacijaStolova.BrojOsoba = model.BrojOsoba;
            rezervacijaStolova.BrojTelefona = model.BrojTelefona;
            rezervacijaStolova.VrijemeRezervacije = model.VrijemeRezervacije;

              
            _context.SaveChanges();
            
            return RedirectToAction(nameof(Index));
        }

        [Area("Admin")]
        [Authorize(Roles = "Admin")]
            public IActionResult Obrisi(int id)
            {
            var ZaBrisanje = _context.RezervacijaStolova.Find(id);
            _context.RezervacijaStolova.Remove(ZaBrisanje);
            _context.SaveChanges();
                
                return RedirectToAction(nameof(Index));
            }
        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult Dodaj(RezervacijeCreteVM model)
        {
            var rezervacija = new RezervacijaStolova
            {
                BrojOsoba = model.BrojOsoba,
                BrojTelefona = model.BrojTelefona,
                Id = model.Id,
                UserName = model.UserName,
                VrijemeRezervacije = model.VrijemeRezervacije
            };
            _context.RezervacijaStolova.Add(rezervacija);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }


    }
}
