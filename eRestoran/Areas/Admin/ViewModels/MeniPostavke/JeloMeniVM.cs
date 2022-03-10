using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRestoran.Areas.Admin.ViewModels
{
    public class JeloMeniVM
    {

        public List<Row> Rows { get; set; }
        public class Row
        {
            public int Id { get; set; }
            public string LinkZaSliku { get; set; }
            public float Cijena { get; set; }
            public string Naziv { get; set; }
            public string Kategorja { get; set; }
            public string Opis { get; set; }
        }
    }
}
