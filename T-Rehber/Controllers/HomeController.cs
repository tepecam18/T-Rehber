using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T_Rehber.Models;

namespace T_Rehber.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            VeritabaniContext Db = new VeritabaniContext();

            //List<Departman> Departmanlar = new List<Departman>()
            //{
            //    new Departman(){DepartmanAdi="İnsan Kaynakları"},
            //    new Departman(){DepartmanAdi="Bilgi İşlem"},
            //    new Departman(){DepartmanAdi="Muhasebe"}
            //};

            //foreach (var departman in Departmanlar)
            //{
            //    Db.Departmanlar.Add(departman);
            //}

            //Db.SaveChanges();

            //List<Calisan> calisans = new List<Calisan>()
            //{
            //    new Calisan(){Adi="aa", Soyadi="bbb", DepartmanID=Db.Departmanlar.Where(i=> i.ID==1).Select(j=> j.ID).FirstOrDefault()},
            //    new Calisan(){Adi="bb", Soyadi="ccc", DepartmanID=Db.Departmanlar.Where(i=> i.ID==1).Select(j=> j.ID).FirstOrDefault()},
            //    new Calisan(){Adi="cc", Soyadi="ddd", DepartmanID=Db.Departmanlar.Where(i=> i.ID==1).Select(j=> j.ID).FirstOrDefault()}
            //};

            //foreach (var item in calisans)
            //{
            //    Db.Calisanlar.Add(item);
            //}

            //Db.SaveChanges();

            List<Calisan> model = new List<Calisan>();
            model = Db.Calisanlar.ToList();
            return View(model);
        }

        public ActionResult Details(int id)
        {
            VeritabaniContext Db = new VeritabaniContext();

            Calisan model = new Calisan();
            model = Db.Calisanlar.Where(i => i.ID == id).FirstOrDefault();

            if (model == null)
            {
                return RedirectToAction("Index");
            }

            model.Departman = Db.Calisanlar.Where(i => i.ID == id).Select(j=> j.Departman).FirstOrDefault();
            model.Yonetici = Db.Calisanlar.Where(i => i.ID == id).Select(j=> j.Yonetici).FirstOrDefault();
            
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string tel, string sifre)
        {
            VeritabaniContext Db = new VeritabaniContext();
            Calisan calisan = new Calisan();

            calisan = Db.Calisanlar.Where(i=> i.Tel == tel && i.Sifre == sifre).FirstOrDefault();

            if (calisan == null)
            {
                ViewBag.str = "active alert alert-danger w-100 text-center";
                return View();
            }
            else
            {
                Session["OturumTipi"] = "Admin";
                Session["OturumID"] = calisan.ID;
                Session.Timeout = 60;
                return RedirectToAction("Index", "Admin");
            }

            
        }
    }
}