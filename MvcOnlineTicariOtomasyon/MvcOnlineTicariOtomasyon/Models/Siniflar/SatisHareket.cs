using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class SatisHareket
    {
        [Key]
        public int Satisid { get; set; }
        public DateTime Tarih { get; set; }

        [Display(Name = "Ürün Adeti")]
        public int Adet { get; set; }

        [Display(Name = "Ürün Fiyatı")]
        public decimal Fiyat { get; set; }

        [Display(Name = "Toplam Tutar")]
        public decimal ToplamTutar { get; set; }

        public int Urunid { get; set; }
       
        public int Cariid { get; set; }
        public int Personelid { get; set; }

        [Display(Name = "Ürün Seçiniz")]
        public virtual Urun Urun { get; set; }

        public virtual Cariler Cariler { get; set; }

        [Display(Name = "Personel Seçiniz")]
        public virtual Personel Personel { get; set; }

    }
}