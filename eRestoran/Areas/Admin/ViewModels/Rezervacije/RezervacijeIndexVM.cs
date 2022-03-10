using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRestoran.Areas.Admin.ViewModels.Rezervacije
{
    public class RezervacijeIndexVM
    {
        public class Row
        {
            public int RezervacijaId { get; set; }
            public string UserName { get; set; }
            
            public int BrojOsoba { get; set; }
           
            public DateTime VrijemeRezervacije { get; set; }
           
            public string BrojTelefona { get; set; }
        }
        public List<Row> Rows { get; set; }
    }
}
