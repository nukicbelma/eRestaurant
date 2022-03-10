using eRestoran.Areas.Uposlenik.ViewModels.Izvjestaj;
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

namespace eRestoran.Areas.Uposlenik.Controllers
{
    public class IzvjestajiController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<OnlineGost> _userManager;
        public IzvjestajiController(ApplicationDbContext context, UserManager<OnlineGost> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Area("Uposlenik")]
        [Authorize(Roles = "Upolsenik")]
        public IActionResult IzvjestajPDF()
        {
            IzvjestajVM pdf = new IzvjestajVM
            {
                ObavijestRows = _context.Obavijestis.Select(i => new IzvjestajVM.RowObavijest
                {
                    Id = i.Id,
                    Poruka = i. Poruka,
                    DatumVrijeme = i.DatumVrijeme.ToString()
                }).ToList()
            };
            return new ViewAsPdf("IzvjestajPDF", pdf);
        }
    }
}
