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
using eRestoran.ViewModels;
using eRestoran.Areas.Admin.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;


namespace eRestoran.Areas.Admin.Controllers
{

    public class MeniPostavkeController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<OnlineGost> _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        
        public MeniPostavkeController(ApplicationDbContext context, UserManager<OnlineGost> userManager,IHostingEnvironment enviroment)
        {
            _context = context;
            _userManager = userManager;
           _hostingEnvironment = enviroment;

        }
        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            
            JeloMeniVM model = new JeloMeniVM
            {
                Rows = (List<JeloMeniVM.Row>)_context.Meni.Select(x => new JeloMeniVM.Row
                {
                    Id = x.Id,
                    Cijena = x.Cijena,
                    Kategorja = x.Kategorija.NazivKategorije,
                    LinkZaSliku = Path.GetFileName(x.LinkZaSliku),
                    Naziv = x.Naziv,
                    Opis = x.Opis
                }).ToList()
            };
            return View(model);

        }

        [Area("Admin")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Uredi(int id)
        {

            Meni meni = _context.Meni.Find(id);

            UrediStavkaVM model = new UrediStavkaVM();
            model.Id = id;
            model.LinkZaSliku = meni.LinkZaSliku;
            model.Naziv = meni.Naziv;
            model.KategorijaId = meni.KategorijaID;
            model.Cijena = meni.Cijena;
            model.Opis = meni.Opis;






            return View(model);
        }
        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult SpasiPromjene(UrediStavkaVM model)
        {
           if(ModelState.IsValid)
            {
 Meni meni = _context.Meni.Find(model.Id);
            //if (meni == null)
            //{
            //    meni = new Meni();
            //    _context.Add(meni);
            //}
            meni.Cijena = model.Cijena;
            meni.Opis = model.Opis;
            meni.Naziv = model.Naziv;
            meni.KategorijaID = model.KategorijaId;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
            }
            else
            {
                // return View("Uredi",model.Id);
                
                return RedirectToAction(nameof(Index));
            }
           

        }
        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult Dodaj()
        {
            var Kategorije = new List<string> { "", "1 - Tjestenina", "2 - Sendviči", "3 - Deserti", "4 - Beefsteak", "5 - Schnitzel", "6 - Predjela" };
            var model = new JeloMeniDodajVM
            {
                Kategorije = Kategorije
                    .ConvertAll
                    (
                        i =>
                        {
                            return new SelectListItem()
                            {
                                Text = i,
                                Value = i,
                                Selected = false
                            };
                        }
                    )

            };
            return View(model); 
        
        }

    
    
    [Area("Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult Obrisi(int id)
        {
            Meni obrisati = _context.Meni.SingleOrDefault(j => j.Id == id);
            _context.Remove(obrisati);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
     //private string SaveFile(IFormFile file)
     //   {
     //       string uploadFileName = null;
     //       string filePath = null;
     //       if(file!=null)
     //       {
     //           string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
     //           uploadFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
     //           filePath = Path.Combine(uploadFolder, uploadFileName);
     //           file.CopyTo(new FileStream(filePath, FileMode.Create));
     //           return "~/images/" + uploadFileName;
     //       }
     //       return null;
     //   }
        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult DodajSave(JeloMeniDodajVM model)
        {
           


                Meni meni = _context.Meni.Find(model.JeloId);
           
                if (meni == null)
                {
                    meni = new Meni();
                    _context.Add(meni);
                }
                string uniqueFileName = null;
                if (model.LinkZaSliku != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    //  string uploadsFolder = ("C:/Users/Hanna Tiro/Desktop/PETI SEMESTAR/Zadaće i seminarski/New folder/eRestoran/wwwroot/images");

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.LinkZaSliku.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.LinkZaSliku.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                meni.LinkZaSliku = uniqueFileName;



                // meni.LinkZaSliku = model.LinkZaSliku;
                meni.Cijena = model.Cijena;
                meni.Opis = model.Opis;
                meni.Naziv = model.Naziv;
                meni.ZaDostavu = "1";

                meni.StanjeNaSkladistu = model.StanjeNaSkladistu;
            

            var ide = model.Kategorija;
            if (ide != null)
            {
                if (ide.Contains("1"))
                    meni.KategorijaID = 1;
                if (ide.Contains("2"))
                    meni.KategorijaID = 2;
                if (ide.Contains("3"))
                    meni.KategorijaID = 3;
                if (ide.Contains("4"))
                    meni.KategorijaID = 4;
                if (ide.Contains("5"))
                    meni.KategorijaID = 5;
                if (ide.Contains("6"))
                    meni.KategorijaID = 6;
            }
               
            
           // var ide = model.Kategorija;
           // meni.KategorijaID = int.Parse(model.Kategorija);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
            //}
            //else
            //{
            //    return View("Dodaj");
            //}
        }

    }
}
