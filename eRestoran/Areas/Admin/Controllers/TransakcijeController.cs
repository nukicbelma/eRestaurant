using eRestoran.Data;
using eRestoran.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using eRestoran.Areas.Admin.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using eRestoran.Areas.Admin.ViewModels.Transakcije;

namespace eRestoran.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TransakcijeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<OnlineGost> _userManager;
        public TransakcijeController(ApplicationDbContext context, UserManager<OnlineGost> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }
        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult OnlineTransakcije()
        {
            
            var model = new OnlineTransakcijeVM
            {
                Rows=_context.OnlineNarudzba.Select(i=>new OnlineTransakcijeVM.Row()
                {
                    Id=i.Id,
                    Cijena=i.Cijena,
                    DatumNarudzbe=i.DatumNarudzbe
                }).ToList()
            };
            return View(model);
        }
        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult SkladisteTransakcije()
        {
            var model = new SkladisteTransakcijeVM
            {
                Rows = _context.UlazUSkladiste.Select(i => new SkladisteTransakcijeVM.Row()
                {
                    Id = i.Id,
                    Cijena = i.Cijena,
                    DatumPrijema = i.DatumPrijema
                }).ToList()
            };
            return View(model);
        }
    }
}
