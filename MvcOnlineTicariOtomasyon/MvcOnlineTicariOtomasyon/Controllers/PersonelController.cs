using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        Context c = new Context();  
        public ActionResult Index()
        {
            var degerler = c.Personels.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.Departmanid.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();  
        }
        [HttpPost]
        public ActionResult PersonelEkle(Personel p)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.PersonelGorsel = "/Image/" + dosyaadi + uzanti;
            }
          c.Personels.Add(p);
          c.SaveChanges();
          return RedirectToAction("Index");   
        }
        public ActionResult PersonelGetir(int? id)
        {
            // Eğer id null ise, Index sayfasına yönlendir
            if (id == null)
            {
                return RedirectToAction("Index"); // Burada Index sayfasına yönlendirme yapılır.
            }

            // Personel bilgilerini ID'ye göre bul
            var personel = c.Personels.Find(id);

            // Eğer personel bulunamazsa, HttpNotFound döndür
            if (personel == null)
            {
                return HttpNotFound("Personel bulunamadı.");
            }

            // Departmanları al
            List<SelectListItem> departmanlar = (from x in c.Departmans.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.DepartmanAd,
                                                     Value = x.Departmanid.ToString()
                                                 }).ToList();
            ViewBag.dgr1 = departmanlar;

            // Personel bilgisiyle birlikte personel güncelleme sayfasını döndür
            return View("PersonelGetir", personel);
        }

        public ActionResult PersonelGuncelle(Personel p)
        {
            // Eğer dosya yüklenmişse
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                var dosya = Request.Files[0];

                // Dosya adı ve uzantısını al
                string dosyaadi = Guid.NewGuid().ToString();  // Benzersiz dosya adı
                string uzanti = Path.GetExtension(dosya.FileName); // Dosya uzantısı
                string yol = "~/Image/" + dosyaadi + uzanti; // Yükleme yolu

                // Klasörün var olup olmadığını kontrol et, yoksa oluştur
                string physicalPath = Server.MapPath("~/Image/");
                if (!Directory.Exists(physicalPath))
                {
                    Directory.CreateDirectory(physicalPath);
                }

                // Dosyayı kaydet
                dosya.SaveAs(Server.MapPath(yol));

                // Kaydedilen dosyanın yolunu güncelle
                p.PersonelGorsel = "/Image/" + dosyaadi + uzanti;
            }
            else
            {
                // Eğer fotoğraf yüklenmemişse, mevcut fotoğrafı koru
                // Veritabanındaki mevcut fotoğrafı al
                var prsn = c.Personels.Find(p.PersonelId);
                p.PersonelGorsel = prsn.PersonelGorsel;  // Eski fotoğrafı korur
            }

            // Veritabanında personel bilgilerini güncelle
            var personel = c.Personels.Find(p.PersonelId);
            personel.PersonelAd = p.PersonelAd;
            personel.PersonelSoyad = p.PersonelSoyad;
            personel.PersonelGorsel = p.PersonelGorsel;
            personel.Departmanid = p.Departmanid;

            // Değişiklikleri kaydet
            c.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult PersonelListe()
        {
            var sorgu = c.Personels.ToList();
            return View(sorgu);
        }

    }
}