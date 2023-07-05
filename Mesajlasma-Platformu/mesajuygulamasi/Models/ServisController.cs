using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MesajlasmaUygulamasi.Models;
using MesajlasmaUygulamasi.ViewModel;

namespace MesajlasmaUygulamasi.Controllers
{
    public class ServisController : ApiController
    {
        DBBB1Entities db = new DBBB1Entities();

        SonucModel sonuc = new SonucModel();


        #region Uyeler
        [HttpGet]
        [Route("api/uyeliste")]
        public List<UyelerModel> UyeListe()
        {
            List<UyelerModel> liste = db.uyeler.Select(x => new UyelerModel()
            {
                uyeId = x.uyeId,
                uyeAdsoyad = x.uyeAdsoyad,
                uyeKadi = x.uyeKadi,
                uyeParola = x.uyeParola,
                uyeMail = x.uyeMail,
                uyeYetki = x.uyeYetki
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/uyebyid/{uyeId}")]
        public UyelerModel UyeById(int uyeId)
        {
            UyelerModel kayit = db.uyeler.Where(s => s.uyeId == uyeId).Select(x => new UyelerModel()
 {
                uyeId = x.uyeId,
                uyeAdsoyad = x.uyeAdsoyad,
                uyeKadi = x.uyeKadi,
                uyeParola = x.uyeParola,
                uyeMail = x.uyeMail,
                uyeYetki = x.uyeYetki
            }).SingleOrDefault();
            return kayit;
        }


        [HttpPost]
        [Route("api/uyeekle")]
        public SonucModel UyeEkle(UyelerModel model)
        {
            if (db.uyeler.Count(s => s.uyeKadi == model.uyeKadi || s.uyeMail == model.uyeMail) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kullanıcı Adı veya E-Posta Adresi Kayıtlıdır!";
                return sonuc;
            }
            uyeler yeni = new uyeler();

            yeni.uyeId = model.uyeId;
            yeni.uyeAdsoyad = model.uyeAdsoyad;
            yeni.uyeKadi = model.uyeKadi;
            yeni.uyeParola = model.uyeParola;
            yeni.uyeMail = model.uyeMail;
            yeni.uyeYetki = model.uyeYetki;

            db.uyeler.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/uyeduzenle")]
        public SonucModel UyeDuzenle(UyelerModel model)
        {
            uyeler kayit = db.uyeler.Where(s => s.uyeId == model.uyeId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }
            kayit.uyeId = model.uyeId;
            kayit.uyeAdsoyad = model.uyeAdsoyad;
            kayit.uyeKadi = model.uyeKadi;
            kayit.uyeParola = model.uyeParola;
            kayit.uyeMail = model.uyeMail;
            kayit.uyeYetki = model.uyeYetki;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/uyesil/{uyeId}")]
        public SonucModel UyeSil(int uyeId)
        {
            uyeler kayit = db.uyeler.Where(s => s.uyeId == uyeId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }
            db.uyeler.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Silindi";
            return sonuc;
        }
        #endregion

        #region Gruplar
        [HttpGet]
        [Route("api/grupliste")]
        public List<GrupModel> GrupListe()
        {
            List<GrupModel> liste = db.gruplar.Select(x => new GrupModel()
            {
                grupId = x.grupId,
                grupAdi = x.grupAdi,

            }).ToList();
            return liste;

        }

        [HttpGet]
        [Route("api/grupbyid/{grupId}")]
        public GrupModel GrupById(int grupId)
        {
            GrupModel kayit = db.gruplar.Where(s => s.grupId == grupId).Select(x =>
            new GrupModel()
            {
                grupId = x.grupId,
                grupAdi = x.grupAdi,

            }).SingleOrDefault();
            return kayit;
        }


        [HttpPost]
        [Route("api/grupekle")]
        public SonucModel GrupEkle(GrupModel model)
        {
            if (db.gruplar.Count(s => s.grupAdi == model.grupAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Grup Adı Kayıtlıdır.";
                return sonuc;
            }
            gruplar yeni = new gruplar();
            yeni.grupAdi = model.grupAdi;
            db.gruplar.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Grup Eklendi";
            return sonuc;

        }


        [HttpPut]
        [Route("api/grupduzenle")]
        public SonucModel GrupDuzenle(GrupModel model)
        {
            gruplar kayit = db.gruplar.Where(s => s.grupId ==
            model.grupId).FirstOrDefault();

            if (kayit == null)
            {

                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;

            }
            kayit.grupAdi = model.grupAdi;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Grup Düzenlendi";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/grupsil/{grupId}")]
        public SonucModel GrupSil(int grupId)
        {
            gruplar kayit = db.gruplar.Where(s => s.grupId == grupId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;

            }
            if (db.mesajlar.Count(s => s.mesajGrupId == grupId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Mesaj Kayıtlı Grup Silinemez!";
                return sonuc;
            }
            db.gruplar.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Grup Silindi";
            return sonuc;
        }



        #endregion

        #region Mesajlar

        [HttpGet]
        [Route("api/mesajliste")]
        public List<MesajModel> MesajListe()
        {
            List<MesajModel> liste = db.mesajlar.Select(x => new MesajModel()
            {
                mesajId = x.mesajId,
                mesajMetni = x.mesajMetni,
                mesajTarih = x.mesajTarih,
                mesajGrupId = x.mesajGrupId,
                mesajUyeId = x.mesajUyeId

            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/mesajlistebyuyeid/{uyeId}")]
        public List<MesajModel> MesajListeByUyeId(int uyeId)
        {
            List<MesajModel> liste = db.mesajlar.Where(s => s.mesajUyeId == uyeId).Select(x => new MesajModel()
 {
                mesajId = x.mesajId,
                mesajMetni = x.mesajMetni,
                mesajTarih = x.mesajTarih,
                mesajGrupId = x.mesajGrupId,
                mesajUyeId = x.mesajUyeId
            }).ToList();
            return liste;
        }

        [HttpPost]
        [Route("api/mesajekle")]
        public SonucModel MesajEkle(MesajModel model)
        {
            if (db.mesajlar.Count(s => s.mesajUyeId == model.mesajUyeId && s.mesajMetni == model.mesajMetni) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Aynı Kişi, Aynı Makaleye Aynı Yorumu Yapamaz!";
                return sonuc;
            }
            mesajlar yeni = new mesajlar();
            yeni.mesajId = model.mesajId;
            yeni.mesajMetni = model.mesajMetni;
            yeni.mesajTarih = model.mesajTarih;
            yeni.mesajGrupId = model.mesajGrupId;
            yeni.mesajUyeId = model.mesajUyeId;
            db.mesajlar.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yorum Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/mesajduzenle")]
        public SonucModel MesajDuzenle(MesajModel model)
        {

            mesajlar kayit = db.mesajlar.Where(s => s.mesajId == model.mesajId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "mesaj bulunamadı";
                return sonuc;

            }
            kayit.mesajId = model.mesajId;
            kayit.mesajMetni = model.mesajMetni;
            kayit.mesajTarih = model.mesajTarih;
            kayit.mesajGrupId = model.mesajGrupId;
            kayit.mesajUyeId = model.mesajUyeId;

            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Düzenlendi!";


            return sonuc;
        }




        [HttpDelete]
        [Route("api/mesajsil/{mesajId}")]
        public SonucModel MesajSil(int mesajId)
        {
            mesajlar kayit = db.mesajlar.Where(s => s.mesajId == mesajId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Mesaj bulunamadı";
                return sonuc;

            }

            db.mesajlar.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Mesaj Silindi";
            return sonuc;


        }

        #endregion
    }
}
 