using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace T_Rehber.Models
{
    public class Calisan
    {
        public int ID { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Tel { get; set; }
        public string Sifre { get; set; }
        public int? DepartmanID { get; set; }
        public int? YoneticiID { get; set; }


        public Departman Departman { get; set; }
        public Calisan Yonetici { get; set; }
        public List<Calisan> Yoneticiler { get; set; }
    }
}