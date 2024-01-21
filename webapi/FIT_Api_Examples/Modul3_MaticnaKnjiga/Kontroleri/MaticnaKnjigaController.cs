using System.Collections.Generic;
using FIT_Api_Examples.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using FIT_Api_Examples.Modul3_MaticnaKnjiga.Models;
using Microsoft.EntityFrameworkCore;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.Modul2.ViewModels;
using FIT_Api_Examples.Modul3_MaticnaKnjiga.ViewModels;

namespace FIT_Api_Examples.Modul3_MaticnaKnjiga.Kontroleri
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MaticnaKnjigaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public MaticnaKnjigaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        [HttpGet]
        public ActionResult GetById(int id)
        {
            var upisAkGodine = _dbContext.UpisGodine
                .Where(s => s.StudentId == id)
                .Select(x => new
                {
                    upisGodina_id = x.Id,
                    akademska_godina_opis = x.AkademskaGodina.opis,
                    x.GodinaStudija,
                    x.IsObnova,
                    x.DatumUpisaZimski,
                    x.DatumOvjere,
                    evidentirao_korisnik = x.KorisnickiNalog.korisnickoIme,
                    x.CijenaSkolarine


                });
                
                

            var PovratnaVr = _dbContext.Student.Where(ks=>ks.id == id)
                .Select(ks=>new
                {
                    student_id = ks.id,
                    ks.ime,
                    ks.prezime,
                    studentId = ks.id,
                    listaUpisi = upisAkGodine.ToList()


                })
                .FirstOrDefault();



            return Ok(PovratnaVr);


        }
        [HttpPost]
        public ActionResult Add([FromBody] MaticnaKnjigaAddVM request)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");
            var evidentirao = _dbContext.KorisnickiNalog.Find(HttpContext.GetLoginInfo().korisnickiNalog.id);
               
                var podaci = new UpisGodine()
                {
                    KorisnickiNalogId = evidentirao.id,
                    AkademskaGodinaId = request.akademska_godina_id,
                    StudentId = request.student_id,
                    CijenaSkolarine = request.cijenaSkolarine,
                    DatumOvjere = request.datumOvjere,
                    DatumUpisaZimski = request.datumUpisaZimski,
                    GodinaStudija = request.godinaStudija,
                    IsObnova = request.isObnova
                };

                _dbContext.Add(podaci);
                _dbContext.SaveChanges();




            return Ok(podaci);
        }
        [HttpPut]
        public ActionResult Ovjeri([FromBody] MaticnaKnjigaOvjeriVM request)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");

            var evidentirao = _dbContext.KorisnickiNalog.Find(HttpContext.GetLoginInfo().korisnickiNalog.id);

            var podaci = _dbContext.UpisGodine.Find(request.Id);
            podaci.KorisnickiNalogId = evidentirao.id;
            podaci.Napomena = request.Napomena;
            podaci.DatumOvjere = request.datumOvjere;

            _dbContext.SaveChanges();

            return Ok(podaci);
        }

    }
}
