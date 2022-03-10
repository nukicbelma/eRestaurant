using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRestoran.Areas.Uposlenik.ViewModels
{
    public class RezervacijeAngularVM
    {
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int id { get; set; }
            public string username { get; set; }
            public int brojOsoba { get; set; }
            public DateTime vrijemeRezervacije { get; set; }
            public string brojTelefona { get; set; }
        }
    }
}
