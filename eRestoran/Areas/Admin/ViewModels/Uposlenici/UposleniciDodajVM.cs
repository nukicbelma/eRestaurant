using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eRestoran.Areas.Admin.ViewModels.Uposlenici
{
    public class UposleniciCreateVM
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Titula { get; set; }

       // public List<SelectListItem> Restorani { get; set; }
        
        //public int RestoranID { get; set; }

        

    }
}
