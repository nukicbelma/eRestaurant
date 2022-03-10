using eRestoran.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eRestoran.Areas.Admin.ViewModels
{
    public class JeloMeniDodajVM
    {
        public int JeloId { get; set; }
       public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Kategorije { get; set; }
        public string Kategorija { get; set; }
        // public IEnumerable<Kategorija> Kategorije { get; set; }
       
        public string Naziv { get; set; }

        public string Opis { get; set; }
        
        public float Cijena { get; set; }
        public float StanjeNaSkladistu { get; set; }
        public IFormFile LinkZaSliku { get; set; }

        
    }
}
