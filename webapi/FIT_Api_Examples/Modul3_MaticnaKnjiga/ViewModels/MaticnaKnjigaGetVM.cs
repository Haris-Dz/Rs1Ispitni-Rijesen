using System;
using FIT_Api_Examples.Modul0_Autentifikacija.Models;
using FIT_Api_Examples.Modul2.Models;
using FIT_Api_Examples.Modul3_MaticnaKnjiga.Models;

namespace FIT_Api_Examples.Modul3_MaticnaKnjiga.ViewModels
{
    public class MaticnaKnjigaGetVM
    {
        public int StudentId { get; set; }
        public string StudentIme { get; set;}
        public string StudentPrezime { get; set;}
        public AkademskaGodina AakademskaGodina { get; set; }
        public bool IsObnova { get; set; }
        public DateTime DatumUpisaZimski { get; set; }
        public DateTime? DatumOvjera { get; set; }
        public KorisnickiNalog Evidentirao { get; set; }
        public string Napomena { get; set; }
        public float Cijena { get; set; }
        public int GodinaStudija { get; set; }
        public Student Student { get; set; }
    }
}
