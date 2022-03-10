using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eRestoran.Models
{
    public class RezervacijaStolova
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [Display(Name = "Broj osoba")]
        //[Required(ErrorMessage = "Unesite broj osoba.")]
        [RegularExpression("[1-9]|10", ErrorMessage = "Unesite broj 0-10.")]
        public int BrojOsoba { get; set; }
        [Display(Name = "Vrijeme rezervacije")]
        public DateTime VrijemeRezervacije { get; set; }

        [Display(Name = "Broj telefona")]
        [RegularExpression("^\\s*\\+?\\s*([0-9][\\s-]*){9,}$", ErrorMessage = "Broj telefona mora sadrzavati 9 cifara.")]
        public string BrojTelefona { get; set; }
    }
}
