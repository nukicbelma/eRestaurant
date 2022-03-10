using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRestoran.Areas.Admin.ViewModels.Izvjestaj
{
    public class IzvjestajVM
    {
        public List<RowDimal> DimalRows { get; set; }
        public List<RowKonzum> KonzumRows { get; set; }
        public List<RowBarila> BarilaRows { get; set; }


        public class RowDimal
        {
            public int StavkaId { get; set; }
            public string Kolicina { get; set; }
            public int UlazUSkladisteID { get; set; }
        }
        public class RowKonzum
        {
            public int StavkaId { get; set; }
            public string Kolicina { get; set; }
            public int UlazUSkladisteID { get; set; }
        }
        public class RowBarila
        {
            public int StavkaId { get; set; }
            public string Kolicina { get; set; }
            public int UlazUSkladisteID { get; set; }
        }
    }
}
