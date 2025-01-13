using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class DepartmanController : Controller
    {
        // GET: Departman
        Context c = new Context();
        
        public ActionResult Index()
        {
            var degerler = c.Departmans.Where(x => x.Durum == true).ToList();
            return View(degerler);
        }
        [HttpGet]
        [Authorize(Roles = "T")]
        public ActionResult DepartmanEkle()
        {
           return View();
        }

        [HttpPost]
        
        public ActionResult DepartmanEkle(Departman d)
        {
            d.Durum = true;
            c.Departmans.Add(d);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanSil(int id)
        {
          var dep = c.Departmans.Find(id);
          dep.Durum = false;
          c.SaveChanges();
          return RedirectToAction("Index");
        }
        public ActionResult DepartmanGetir(int? id)  // id'yi nullable hale getirdik
        {
            if (id == null)  // id boşsa yönlendirme yapılacak
            {
                return RedirectToAction("Index");
            }

            var dpt = c.Departmans.Find(id);  // Departman id'ye göre arama yapılır

            if (dpt == null)  // Eğer departman bulunamazsa, 404 hata sayfasına yönlendirilir
            {
                return HttpNotFound();
            }

            return View("DepartmanGetir", dpt);  // Departman bilgisi View'a gönderilir
        }

        public ActionResult DepartmanGuncelle(Departman p)
        {
          var dept = c.Departmans.Find(p.Departmanid);
          dept.DepartmanAd = p.DepartmanAd;
          c.SaveChanges();
          return RedirectToAction("Index");
        }
        public ActionResult DepartmanDetay(int id)
        {
            var degerler = c.Personels.Where(x => x.Departmanid == id).ToList();
            var dpt = c.Departmans.Where(x => x.Departmanid == id).Select(y => y.DepartmanAd).FirstOrDefault();
            ViewBag.d = dpt;
            return View(degerler);
        }
        public ActionResult DepartmanPersonelSatis(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.Personelid == id).ToList();  
            var per = c.Personels.Where(x => x.PersonelId==id).Select(y => y.PersonelAd + " " + y.PersonelSoyad).FirstOrDefault();
            ViewBag.dpers = per;
            return View(degerler);
        }
    }
}