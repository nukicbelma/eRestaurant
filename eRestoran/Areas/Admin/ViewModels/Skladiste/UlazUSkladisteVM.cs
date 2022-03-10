using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRestoran.Areas.Admin.ViewModels.Skladiste
{
    public class UlazUSkladisteVM
    {
        public class Row
        {
            public int Id { get; set; }
            public string Naziv { get; set; }
            public float KolicinaUlaza { get; set; }
            public string ImeDobavljaca { get; set; }
            public DateTime DatumPrijema { get; set; }
            public int UposlenikID { get; set; }
        }
        public List<Row> Rows { get; set; }
    }
}
