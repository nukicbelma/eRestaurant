using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eRestoran.Areas.Uposlenik.ViewModels
{
    public class DodajRezervacijuVM
    {
        public int id { get; set; }
        public string username { get; set; }
        [Required(ErrorMessage = "Unesite broj osoba.")]
        [RegularExpression("^(?(?=9)9(\\.0+)?|([1-9]\\d?(\\.\\d+)?))$", ErrorMessage = "Unesite broj osoba od 0 do 10.")]
        public int brojOsoba { get; set; }
        public DateTime vrijemeRezervacije { get; set; }
        [RegularExpression("^\\s*\\+?\\s*([0-9][\\s-]*){9,}$", ErrorMessage = "Broj telefona mora sadrzavati 9 cifara.")]
        public string brojTelefona { get; set; }
    }
}
