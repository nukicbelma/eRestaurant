using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eRestoran.Areas.Admin.ViewModels
{
    public class UrediStavkaVM
    {
        //  public int StavkaId { get; set; }
        //  public string Naziv { get; set; }
        // public string LinkZaSliku { get; set; }
        //  public string Opis { get; set; }
        //  //public string Kategorija { get; set; }
        ////  public int KategorijaId { get; set; }
        ///
        public int Id { get; set; }
        public string LinkZaSliku { get; set; }
        [Required(ErrorMessage = "Cijena je obavezno polje")]
        [RegularExpression("^(?(?=99)99(\\.0+)?|([1-9]\\d?(\\.\\d+)?))$", ErrorMessage = "Cijena mora biti veća od 0 i manja od 100")]
        public float Cijena { get; set; }
        [Required(ErrorMessage = "Naziv jela je obavezno polje")]
        [RegularExpression("^.{4,}$", ErrorMessage = "Opis mora imat više od 4 slova")]
        public string Naziv { get; set; }
        public int KategorijaId { get; set; }

        [Required(ErrorMessage = "Opis je obavezno polje")]
        [RegularExpression("^.{6,}$", ErrorMessage = "Opis mora imat više od 6 slova")]
        public string Opis { get; set; }

    }
}
