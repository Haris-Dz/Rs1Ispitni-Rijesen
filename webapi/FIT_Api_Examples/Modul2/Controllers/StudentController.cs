using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.Modul0_Autentifikacija.Models;
using FIT_Api_Examples.Modul2.Models;
using FIT_Api_Examples.Modul2.ViewModels;
using FIT_Api_Examples.Modul3_MaticnaKnjiga.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIT_Api_Examples.Modul2.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        [HttpGet]
        public ActionResult GetById(int id)
        {
            var student = _dbContext.Student.FirstOrDefault(x => x.id == id);

            return Ok(student);
        }


        [HttpGet]
        public ActionResult<List<Student>> GetAll(string ime_prezime)
        {
            var data = _dbContext.Student
                .Include(s => s.opstina_rodjenja.drzava)
                .Where(x => x.obrisan == false && ime_prezime == null || (x.ime + " " + x.prezime).StartsWith(ime_prezime) || (x.prezime + " " + x.ime).StartsWith(ime_prezime))
                .OrderByDescending(s => s.id)
                .AsQueryable();
            return data.Take(100).ToList();
        }
        [HttpPost]
        public ActionResult Add([FromBody] StudentAddVM request)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");
            if (request.Id == 0)
            {
                var student = new Student()
                {
                    ime = request.ime,
                    prezime = request.prezime,
                    opstina_rodjenja_id = request.opstina_rodjenja_id
                };

                _dbContext.Add(student);
                _dbContext.SaveChanges();
            }
            else
            {
                var student = _dbContext.Student.Find(request.Id);
                student.ime=request.ime;
                student.prezime=request.prezime;
                student.opstina_rodjenja_id = request.opstina_rodjenja_id;
                _dbContext.SaveChanges();
            }
  


            return Ok();
        }

        [HttpPut]
        public ActionResult Obrisi([FromBody] StudentObrisiVM request)
        {
            if (!HttpContext.GetLoginInfo().isLogiran)
                return BadRequest("nije logiran");

            if (request.Id != null)
            {
                var student = _dbContext.Student.Find(request.Id);
                student.obrisan = request.obrisan;
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Pogresan ID");
            }
            

            return Ok();
        }
        

    }
}
