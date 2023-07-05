using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MesajlasmaUygulamasi.ViewModel
{
    public class UyelerModel
    {
        public int uyeId { get; set; }
        public string uyeAdsoyad { get; set; }
        public string uyeKadi { get; set; }
        public string uyeParola { get; set; }
        public string uyeMail { get; set; }
        public int uyeYetki { get; set; }
    }
}