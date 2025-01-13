using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        Context c = new Context();  
        public ActionResult Index()
        {
            var degerler = c.SatisHarekets.ToList();    
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> deger1 =(from x in c.Uruns.ToList()
                                          select new SelectListItem
                                          {
                                              Text=x.UrunAd,
                                              Value=x.Urunid.ToString()
                                          }).ToList();

            List<SelectListItem> deger2 = (from x in c.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CariSoyad,
                                               Value = x.Cariid.ToString()
                                           }).ToList();

            List<SelectListItem> deger3 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelId.ToString()
                                           }).ToList();

            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            return View();
        }
        [HttpPost]  
        public ActionResult YeniSatis(SatisHareket s)
        {
            s.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(s);
            c.SaveChanges();
            return RedirectToAction("Index");   
        }
        public ActionResult SatisGetir(int? id)
        {
            // Eğer id null ise, Index sayfasına yönlendir
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            // Veritabanından satış verisini al
            var satisDeger = c.SatisHarekets.Find(id);

            // Satış bulunamazsa, hata sayfası döndür
            if (satisDeger == null)
            {
                return HttpNotFound("Satış bulunamadı.");
            }

            // Ürünleri al ve ViewBag'e gönder
            List<SelectListItem> deger1 = c.Uruns
                                          .Select(x => new SelectListItem
                                          {
                                              Text = x.UrunAd,
                                              Value = x.Urunid.ToString()
                                          }).ToList();

            // Carileri al ve ViewBag'e gönder
            List<SelectListItem> deger2 = c.Carilers
                                          .Select(x => new SelectListItem
                                          {
                                              Text = x.CariAd + " " + x.CariSoyad,
                                              Value = x.Cariid.ToString()
                                          }).ToList();

            // Personelleri al ve ViewBag'e gönder
            List<SelectListItem> deger3 = c.Personels
                                          .Select(x => new SelectListItem
                                          {
                                              Text = x.PersonelAd + " " + x.PersonelSoyad,
                                              Value = x.PersonelId.ToString()
                                          }).ToList();

            // ViewBag'e gerekli verileri gönderiyoruz
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;

            // Satış verisiyle birlikte View'a yönlendir
            return View("SatisGetir", satisDeger);
        }


        public ActionResult SatisGuncelle(SatisHareket p)
        {
            var deger = c.SatisHarekets.Find(p.Satisid);
            deger.Cariid = p.Cariid;
            deger.Adet = p.Adet;
            deger.Fiyat = p.Fiyat;
            deger.Personelid = p.Personelid;
            deger.Tarih = p.Tarih;
            deger.ToplamTutar = p.ToplamTutar;
            deger.Urunid = p.Urunid;
            c.SaveChanges();
            return RedirectToAction("Index");   
        }
        public ActionResult SatisDetay(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.Satisid == id).ToList();
            return View(degerler);
        }
    }
}