using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRestoran.Areas.Admin.ViewModels.Skladiste
{
    public class StavkaUlazaVM
    {
        public class Row
        {
   public int Id { get; set; }
        public string Kolicina { get; set; }  
        public int MeniID { get; set; }
        public string NazivJela { get; set; }
        }
        public List<Row> Rows { get; set; }

    }
}
