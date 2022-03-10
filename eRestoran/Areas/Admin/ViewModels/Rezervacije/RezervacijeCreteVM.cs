using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRestoran.Areas.Admin.ViewModels.Rezervacije
{
    public class RezervacijeCreteVM
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public int BrojOsoba { get; set; }

        public DateTime VrijemeRezervacije { get; set; }

        public string BrojTelefona { get; set; }
    }
}
