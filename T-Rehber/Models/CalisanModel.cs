using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_Rehber.Models
{
    public class CalisanModel
    {
        public List<Calisan> Calisanlar { get; set; }
        public List<Departman> Departmanlar { get; set; }
        public Calisan Calisan { get; set; }
        public Departman Departman { get; set; }
    }
}