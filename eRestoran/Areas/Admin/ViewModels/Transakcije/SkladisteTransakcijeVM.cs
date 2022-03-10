using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRestoran.Areas.Admin.ViewModels.Transakcije
{
    public class SkladisteTransakcijeVM
    {
        public class Row
        {
            public int Id { get; set; }
        
            public float Cijena { get; set; }
            public DateTime DatumPrijema { get; set; }
            
        }
        public List<Row> Rows { get; set; }
    }
}
