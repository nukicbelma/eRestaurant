using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eRestoran.Data;
using eRestoran.Models;
using eRestoran.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Mvc.Core;


namespace eRestoran.Controllers
{
    public class NarudzbeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<OnlineGost> _userManager;
        private readonly SignInManager<OnlineGost> _signInManager;

        public NarudzbeController(ApplicationDbContext context, UserManager<OnlineGost> userManager, SignInManager<OnlineGost> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [AllowAnonymous]
        public IActionResult Index(int? page)
        {
            //var appUser = await _userManager.GetUserAsync(User);
            //List<string> narudzbeUserNameovi = _context.OnlineNarudzba.Select(x => x.Ime).ToList();
            //if (narudzbeUserNameovi.Contains(appUser.Ime))
            //{
                List<OnlineNarudzbaVM.Row> vmNarudzba = _context.OnlineNarudzba.Select(x => new OnlineNarudzbaVM.Row
                { 
                    NarudzbaID = x.Id,
                    Ime = x.Ime,
                    Prezime = x.Prezime,
                    Adresa = x.Adresa,
                    Cijena = x.Cijena,
                    DatumNarudzbe = x.DatumNarudzbe,
                    Telefon = x.Telefon,
                    NazivJela = x.NazivJela
                }).ToList();
                //return View(vmNarudzba.ToPagedList(page ?? 1, 3));
                return Ok(Json(vmNarudzba));
            //}
            //else
            //{
            //    //return View();
            //    return Ok();
            //}
        }
        [Authorize(Roles = "Korisnik")]
        public async Task<IActionResult> SnimiAsync(int id)
        {
            var appUser = await _userManager.GetUserAsync(User);
            OnlineNarudzba onlineNarudzba = new OnlineNarudzba
            {
                Ime = appUser.Ime,
                Prezime = appUser.Prezime,
                Adresa = appUser.Adresa,
                DatumNarudzbe = DateTime.Now,
                Telefon = appUser.PhoneNumber,
                NazivJela = _context.Meni.Where(x => x.Id == id).Select(x => x.Naziv).FirstOrDefault(),
                Cijena = _context.Meni.Where(x => x.Id == id).Select(x => x.Cijena).FirstOrDefault()
            };
            _context.Add(onlineNarudzba);
            _context.SaveChanges();

            return RedirectToAction("Index","Meni");
        }
        [Authorize(Roles = "Korisnik")]

        // GET: Rezervacije/Delete/5
        [AllowAnonymous]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onlineNarudzba = await _context.OnlineNarudzba
                .FirstOrDefaultAsync(m => m.Id == id);
            if (onlineNarudzba == null)
            {
                return NotFound();
            }

            return View(onlineNarudzba);
        }

        public async Task<IActionResult> Dodaj(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jelo = await _context.Meni
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jelo == null)
            {
                return NotFound();
            }

            return View(jelo);
        }
        [Authorize(Roles = "Korisnik")]

        // POST: Rezervacije/Delete/5
        [HttpDelete, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var onlineNarudzba = await _context.OnlineNarudzba.FindAsync(id);
            _context.OnlineNarudzba.Remove(onlineNarudzba);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public IActionResult ObrisiAngular(int id)
        {
            var onlineNarudzba = _context.OnlineNarudzba.Find(id);
            _context.OnlineNarudzba.Remove(onlineNarudzba);
            _context.SaveChanges();
            List<OnlineNarudzbaVM.Row> vmNarudzba = _context.OnlineNarudzba.Select(x => new OnlineNarudzbaVM.Row
            {
                NarudzbaID = x.Id,
                Ime = x.Ime,
                Prezime = x.Prezime,
                Adresa = x.Adresa,
                Cijena = x.Cijena,
                DatumNarudzbe = x.DatumNarudzbe,
                Telefon = x.Telefon,
                NazivJela = x.NazivJela
            }).ToList();
            return Ok(Json(vmNarudzba));
        }
    }
}
