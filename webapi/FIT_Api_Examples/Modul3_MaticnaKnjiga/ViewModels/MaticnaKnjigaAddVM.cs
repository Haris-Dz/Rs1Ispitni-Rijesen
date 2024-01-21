using FIT_Api_Examples.Modul0_Autentifikacija.Models;
using FIT_Api_Examples.Modul2.Models;
using FIT_Api_Examples.Modul3_MaticnaKnjiga.Models;
using System;

namespace FIT_Api_Examples.Modul3_MaticnaKnjiga.ViewModels
{
    public class MaticnaKnjigaAddVM
    {
        public int student_id { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public int akademska_godina_id { get; set; }
        public bool isObnova { get; set; }
        public DateTime datumUpisaZimski { get; set; }
        public DateTime? datumOvjere { get; set; }
        public string evidentirao_korisnik { get; set; }
        public float cijenaSkolarine { get; set; }
        public int godinaStudija { get; set; }
    }
}
