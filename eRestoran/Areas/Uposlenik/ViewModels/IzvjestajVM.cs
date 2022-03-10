using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRestoran.Areas.Uposlenik.ViewModels.Izvjestaj
{
    public class IzvjestajVM
    {
        public List<RowObavijest> ObavijestRows { get; set; }
        public class RowObavijest
        {
            public int Id { get; set; }

            public string Poruka { get; set; }
            public string DatumVrijeme { get; set; }
        }
    }
}

