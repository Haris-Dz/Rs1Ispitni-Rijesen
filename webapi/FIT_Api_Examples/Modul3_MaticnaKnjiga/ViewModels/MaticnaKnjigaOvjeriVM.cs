using System;

namespace FIT_Api_Examples.Modul3_MaticnaKnjiga.ViewModels
{
    public class MaticnaKnjigaOvjeriVM
    {
        public int Id { get; set; }
        public string evidentirao_korisnik { get; set; }
        public DateTime? datumOvjere { get; set; }
        public string Napomena { get; set; }
    }
}
