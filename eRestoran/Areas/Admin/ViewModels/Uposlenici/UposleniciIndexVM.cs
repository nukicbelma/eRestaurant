using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRestoran.Areas.Admin.ViewModels.Uposlenici
{
    public class UposleniciIndexVM
    {
       
        public class Row
        {
            public int UposlenikId { get; set; }
            public string ImePrezime { get; set; }
            public string Titula { get; set; }
            public string Restoran { get; set; }
        }
        public List<Row> Rows { get; set; }
    }
}
