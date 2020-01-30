using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T_Rehber.Models
{
    public class Departman
    {
        public int ID { get; set; }
        public string DepartmanAdi { get; set; }

        public List<Calisan> Calisanlar { get; set; }
    }
}