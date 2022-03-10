using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRestoran.Areas.Admin.ViewModels.Uposlenici
{
    public class UposleniciAngularVM
    {
        public class Row
        {
 public int uposlenikId { get; set; }
            public string ime { get; set; }
            public string prezime { get; set; }
            public string imePrezime { get; set; }

            public string titula { get; set; }
            public string restoran { get; set; }
        }
        public List<Row> Rows { get; set; }


    }
}

