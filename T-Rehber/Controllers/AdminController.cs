using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T_Rehber.Models;

namespace T_Rehber.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            //mevcut oturumun olup olmadıgının kontrolü
            if (Session["OturumTipi"] == null)
            {
                return OturumKapat();
            }

            VeritabaniContext Db = new VeritabaniContext();
            List<Calisan> model = new List<Calisan>();

            model = Db.Calisanlar.ToList();

            //calısanlar üzerinden departman adını alırken
            //alınan null hatasını düzeltmek için
            foreach (var item in model)
            {
                item.Departman = Db.Departmanlar.Where(i => i.ID == item.DepartmanID).FirstOrDefault();
            }
            return View(model);
        }

        public ActionResult UyeEkle(int? ID)
        {
            //mevcut oturumun olup olmadıgının kontrolü
            if (Session["OturumTipi"] == null)
            {
                return OturumKapat();
            }

            VeritabaniContext Db = new VeritabaniContext();
            CalisanModel model = new CalisanModel();

            model.Departmanlar = Db.Departmanlar.ToList();
            model.Calisanlar = Db.Calisanlar.ToList();

            //yönetici seçimi için ad ile soyadı birlestiriyor
            foreach (var item in model.Calisanlar)
            {
                item.Adi += " " + item.Soyadi;
            }

            if (ID == null)
            {
                model.Calisan = new Calisan
                {
                    Adi = "",
                    Soyadi = "",
                    Tel = ""
                };
                ViewBag.calisan = "active";
            }
            else
            {
                VeritabaniContext Db2 = new VeritabaniContext();
                model.Calisan = Db2.Calisanlar.Where(i => i.ID == ID).FirstOrDefault();

            }

            ViewBag.kaydedildi = "fade";

            return View(model);
        }

        [HttpPost]
        public ActionResult UyeEkle(string ad, string soyad, string tel, string sifre, int? departman, int? yonetici, int? ID)
        {
            //mevcut oturumun olup olmadıgının kontrolü
            if (Session["OturumTipi"] == null)
            {
                return OturumKapat();
            }

            //üzerinde oynma yapılan veritabanı
            VeritabaniContext Db = new VeritabaniContext();
            //temiz veritabanı baglantısı
            VeritabaniContext Db2 = new VeritabaniContext();
            CalisanModel model = new CalisanModel();

            model.Departmanlar = Db.Departmanlar.ToList();
            model.Calisanlar = Db.Calisanlar.ToList();

            //yönetici seçimi için ad ile soyadı birlestiriyor
            foreach (var item in model.Calisanlar)
            {
                item.Adi += " " + item.Soyadi;
            }


            if (soyad == null)
            {
                model.Calisan = Db2.Calisanlar.Where(i => i.ID == ID).FirstOrDefault();
                ViewBag.kaydedildi = "fade";
                return View(model);
            }
            else if (ID != null)
            {
                var calısan = Db2.Calisanlar.Where(i => i.ID == ID).FirstOrDefault();

                if (calısan != null)
                {
                    calısan.Adi = ad;
                    calısan.Soyadi = soyad;
                    calısan.Tel = tel;
                    if (sifre != "***")
                        calısan.Sifre = sifre;
                    if (departman != null)
                        calısan.DepartmanID = departman;
                    if (yonetici != null)
                        calısan.YoneticiID = yonetici;
                }

                Db2.SaveChanges();

                model.Calisan = Db2.Calisanlar.Where(i => i.ID == ID).FirstOrDefault();

                return View(model);
            }
            else
            {

                model.Calisan = new Calisan
                {
                    Adi = ad,
                    Soyadi = soyad,
                    Tel = tel,
                    Sifre = sifre
                };

                //departman ve yönetici kontolü için
                if (departman != null)
                    model.Calisan.DepartmanID = departman;

                if (yonetici != -1)
                    model.Calisan.YoneticiID = yonetici;

                Db2.Calisanlar.Add(model.Calisan);
                Db2.SaveChanges();

                return View(model);

            }

        }

        [HttpPost]
        public ActionResult UyeSil(int ID)
        {
            //mevcut oturumun olup olmadıgının kontrolü
            if (Session["OturumTipi"] == null)
            {
                return OturumKapat();
            }

            VeritabaniContext Db = new VeritabaniContext();

            int id = (int)Session["OturumID"];


            //silen kişinin yöneticisi varsa silmeye izin vermemek için
            if (Db.Calisanlar.Where(i => i.ID == id).FirstOrDefault().YoneticiID != null)
            {
                return RedirectToAction("Index");
            }

            //yöneticiyse silinmeye izin vermemek için
            if (Db.Calisanlar.Where(i => i.YoneticiID == ID).FirstOrDefault() != null)
            {
                return RedirectToAction("Index");
            }

            var calısan = Db.Calisanlar.Where(i => i.ID == ID).FirstOrDefault();

            if (calısan != null)
            {
                Db.Calisanlar.Remove(calısan);
            }

            Db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Departman(int? ID)
        {
            //mevcut oturumun olup olmadıgının kontrolü
            if (Session["OturumTipi"] == null)
            {
                return OturumKapat();
            }
            VeritabaniContext Db = new VeritabaniContext();
            List<Departman> model = new List<Departman>();

            model = Db.Departmanlar.ToList();
            ViewBag.ID = ID;

            return View(model);
        }

        [HttpPost]
        public ActionResult DepatmanEkle(string dAdi)
        {
            //mevcut oturumun olup olmadıgının kontrolü
            if (Session["OturumTipi"] == null)
            {
                return OturumKapat();
            }

            if (dAdi != null || dAdi != "")
            {
                VeritabaniContext Db = new VeritabaniContext();
                Departman departman = new Departman() { DepartmanAdi = dAdi };

                Db.Departmanlar.Add(departman);

                Db.SaveChanges();
            }

            return RedirectToAction("Departman");
        }

        [HttpPost] 
        public ActionResult DepartmanDuzenle(int? ID, string dAdi)
        {
            //mevcut oturumun olup olmadıgının kontrolü
            if (Session["OturumTipi"] == null)
            {
                return OturumKapat();
            }

            if (ID != null)
            {
                //departman adı gönderilmezse silinecek
                //gönderilirse düzenlenecek
                if (dAdi == null || dAdi == "")
                {
                    VeritabaniContext Db = new VeritabaniContext();

                    if (Db.Calisanlar.Where(i=> i.DepartmanID == ID).FirstOrDefault() != null)
                    {
                        return RedirectToAction("Index");
                    }

                    var departman = Db.Departmanlar.Where(i => i.ID == ID).FirstOrDefault();

                    Db.Departmanlar.Remove(departman);

                    Db.SaveChanges();
                }
                else
                {
                    VeritabaniContext Db = new VeritabaniContext();
                    var departman = Db.Departmanlar.Where(i => i.ID == ID).FirstOrDefault();

                    departman.DepartmanAdi = dAdi;

                    Db.SaveChanges();
                }
            }

            return RedirectToAction("Departman");
        }

        public ActionResult OturumKapat()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }
    }
}