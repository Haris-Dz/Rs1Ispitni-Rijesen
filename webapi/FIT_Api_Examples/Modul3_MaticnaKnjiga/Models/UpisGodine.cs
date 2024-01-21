using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FIT_Api_Examples.Modul0_Autentifikacija.Models;
using FIT_Api_Examples.Modul2.Models;

namespace FIT_Api_Examples.Modul3_MaticnaKnjiga.Models
{
    public class UpisGodine
    {
        [Key] 
        public int Id { get; set; }

        [ForeignKey(nameof(KorisnickiNalog))] 
        public int KorisnickiNalogId { get; set; }
        public KorisnickiNalog KorisnickiNalog { get; set; }

        [ForeignKey(nameof(AkademskaGodina))]
        public int AkademskaGodinaId { get; set; }
        public AkademskaGodina AkademskaGodina { get; set; }

        [ForeignKey(nameof(Student))] 
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int GodinaStudija { get; set; }
        public float CijenaSkolarine { get; set; }
        public bool IsObnova { get; set; }
        public DateTime DatumUpisaZimski { get; set; }
        public DateTime? DatumOvjere { get; set; }
        public string Napomena { get; set; }
    }
}
