using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MesajlasmaUygulamasi.ViewModel
{
    public class MesajModel
    {
        public int mesajId { get; set; }
        public string mesajMetni { get; set; }
        public System.DateTime mesajTarih { get; set; }
        public int mesajGrupId { get; set; }
        public int mesajUyeId { get; set; }
    }
}