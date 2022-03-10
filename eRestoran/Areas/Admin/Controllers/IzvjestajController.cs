using eRestoran.Areas.Admin.ViewModels.Izvjestaj;
using eRestoran.Data;
using eRestoran.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eRestoran.Areas.Admin.Controllers
{
    

    public class IzvjestajController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<OnlineGost> _userManager;
        public IzvjestajController(ApplicationDbContext context, UserManager<OnlineGost> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult IzvjestajPDF()
        {
            IzvjestajVM pdf = new IzvjestajVM
            {
                BarilaRows = _context.StavkaUlaza.Include(x => x.UlazUSkladiste).Where(x => x.UlazUSkladiste.ImeDobavljaca == "Barila").Select(i => new IzvjestajVM.RowBarila
                {
                    StavkaId = i.Id,
                    Kolicina = i.Kolicina,
                    UlazUSkladisteID = i.UlazUSkladisteID
                }).ToList(),
                DimalRows = _context.StavkaUlaza.Include(x => x.UlazUSkladiste).Where(x => x.UlazUSkladiste.ImeDobavljaca == "Dimal").Select(i => new IzvjestajVM.RowDimal
                {
                    StavkaId = i.Id,
                    Kolicina = i.Kolicina,
                    UlazUSkladisteID = i.UlazUSkladisteID
                }).ToList(),
                KonzumRows = _context.StavkaUlaza.Include(x => x.UlazUSkladiste).Where(x => x.UlazUSkladiste.ImeDobavljaca == "Konzum").Select(i => new IzvjestajVM.RowKonzum
                {
                    StavkaId = i.Id,
                    Kolicina = i.Kolicina,
                    UlazUSkladisteID = i.UlazUSkladisteID
                }).ToList()


            };
            return new ViewAsPdf("IzvjestajPDF", pdf);
        }
    }
}
