using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegerTestTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntegerTestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarParkController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CarParkController(DatabaseContext context) =>
            _context = context;

        private class CarsList<T>
        {
            public T carId { get; set; }
            public T year { get; set; }
            public T model { get; set; }
            public T stateNumber { get; set; }
            public T registrationDate { get; set; }
        }

        private class TechInspectionList<T>
        {
            public T inspectionId { get; set; }
            public T carId { get; set; }
            public T cardNumber { get; set; }
            public T dateTechInspection { get; set; }
            public T comment { get; set; }
        }

        // GET: api/CarPark/[action]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCars()
        {
            List<CarsList<dynamic>> carlist = new List<CarsList<dynamic>>();
            foreach (var car in await _context.Cars.ToListAsync())
                carlist.Add(new CarsList<dynamic>()
                {
                    carId = car.CarId,
                    year = car.Year,
                    model = car.Model,
                    stateNumber = car.StateNumber,
                    registrationDate = car.RegistrationDate?.ToString("yyyy-MM-dd")
                });

            return Ok(carlist);
        }

        // GET: api/CarPark/[action]/id
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetCar(int id)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.CarId == id);

            if (car == null)
                return BadRequest();

            List<CarsList<dynamic>> carlist = new List<CarsList<dynamic>>();
            carlist.Add(new CarsList<dynamic>()
            {
                carId = car.CarId,
                year = car.Year,
                model = car.Model,
                stateNumber = car.StateNumber,
                registrationDate = car.RegistrationDate?.ToString("yyyy-MM-dd")
            });
            return Ok(carlist);
        }

        // POST: api/CarPark/[action]
        [HttpPost("[action]")]
        public async Task<IActionResult> PostCar([FromBody]Car car)
        {
            if (car == null)
                return BadRequest("Для создания автомобиля необходимо указать его данные");

            if (await _context.Cars.FirstOrDefaultAsync(c => c.StateNumber == car.StateNumber) != null)
                return BadRequest("Автомобиль с таким гос. номером уже есть в базе");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    return BadRequest(e);
                }
                return Ok(car);
            }
            return BadRequest("На вход поступила не валидная модель");
        }

        // PUT: api/CarPark/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> PutCar(int id, [FromBody]Car car)
        {
            if (car == null)
                return BadRequest("Для изменения автомобиля необходимо указать ему новые данные");

            if (ModelState.IsValid)
            {
                try
                {
                    var contextCar = await _context.Cars.FindAsync(id);

                    if (contextCar == null)
                        return BadRequest("Не возможно отредактировать несуществующий в базе автомобиль");

                    if (car.Year == contextCar.Year && car.Model == contextCar.Model
                        && car.StateNumber == contextCar.StateNumber && car.RegistrationDate ==contextCar.RegistrationDate )
                        return BadRequest("Изменений в исходном автомобиле не обнаружено");

                    contextCar.Year = car.Year;
                    contextCar.Model = car.Model;
                    contextCar.StateNumber = car.StateNumber;
                    contextCar.RegistrationDate = car.RegistrationDate;

                    _context.Entry(contextCar).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    return BadRequest(e);
                }
                return Ok();
            }
            return BadRequest("На вход поступила не валидная модель");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            if (id == 0)
                return BadRequest("Не выбран автомобиль для удаления");

            if (ModelState.IsValid)
            {
                try
                {
                    var car = await _context.Cars.FindAsync(id);
                    if (car == null)
                        return BadRequest("Не возможно удалить несуществующий в базе автомобиль");

                    _context.Cars.Remove(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    return BadRequest(e);
                }
                return Ok();
            }
            return BadRequest("На вход поступила не валидная модель");
        }

        // GET: api/CarPark/[action]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTechInspections()
        {
            List<TechInspectionList<dynamic>> techinspectionlist = new List<TechInspectionList<dynamic>>();
            foreach (var techinspection in await _context.TechInspections.ToListAsync())
                techinspectionlist.Add(new TechInspectionList<dynamic>()
                {
                    inspectionId = techinspection.InspectionId,
                    carId = techinspection.CarId,
                    cardNumber = techinspection.CardNumber,
                    dateTechInspection = techinspection.DateTechInspection.ToString("yyyy-MM-dd"),
                    comment = techinspection.Comment
                });

            return Ok(techinspectionlist);
        }

        // GET: api/CarPark/[action]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetTechInspection(int id)
        {
            var techinspection = await _context.TechInspections.FirstOrDefaultAsync(ti => ti.InspectionId == id);

            if (techinspection == null)
                return BadRequest();

            List<TechInspectionList<dynamic>> techinspectionlist = new List<TechInspectionList<dynamic>>();
            techinspectionlist.Add(new TechInspectionList<dynamic>()
            {
                inspectionId = techinspection.InspectionId,
                carId = techinspection.CarId,
                cardNumber = techinspection.CardNumber,
                dateTechInspection = techinspection.DateTechInspection.ToString("yyyy-MM-dd"),
                comment = techinspection.Comment
            });

            return Ok(techinspectionlist);
        }

        // POST: api/CarPark/[action]
        [HttpPost("[action]")]
        public async Task<IActionResult> PostTechInspection([FromBody]TechInspection techInspection)
        {
            if (techInspection == null)
                return BadRequest("Для создания тех. осмотра необходимо указать его данные");

            if (await _context.TechInspections.FirstOrDefaultAsync(ti => ti.CardNumber == techInspection.CardNumber) != null)
                return BadRequest("Тех. осмотор с таким номером карты уже есть в базе");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(techInspection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    return BadRequest(e);
                }
                return Ok(techInspection);
            }
            return BadRequest("На вход поступила не валидная модель");
        }

        // PUT: api/CarPark/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> PutTechInspection(int id, [FromBody]TechInspection techInspection)
        {
            if (techInspection == null)
                return BadRequest("Для изменения тех. осмотра необходимо указать ему новые данные");

            if (ModelState.IsValid)
            {
                try
                {
                    var contextTechInspection = await _context.TechInspections.FindAsync(id);

                    if (contextTechInspection == null)
                        return BadRequest("Не возможно отредактировать несуществующий в базе тех. осмотор");

                    if (techInspection.CarId == contextTechInspection.CarId && techInspection.CardNumber == contextTechInspection.CardNumber
                        && techInspection.DateTechInspection == contextTechInspection.DateTechInspection && techInspection.Comment == contextTechInspection.Comment)
                        return BadRequest("Изменений в исходном тех.осмотре не обнаружено");

                    contextTechInspection.CarId = techInspection.CarId;
                    contextTechInspection.CardNumber = techInspection.CardNumber;
                    contextTechInspection.DateTechInspection = techInspection.DateTechInspection;
                    contextTechInspection.Comment = techInspection.Comment;

                    _context.Entry(contextTechInspection).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    return BadRequest(e);
                }
                return Ok();
            }
            return BadRequest("На вход поступила не валидная модель");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteTechInspection(int id)
        {
            if (id == 0)
                return BadRequest("Не указан тех. осмотор для удаления");

            if (ModelState.IsValid)
            {
                try
                {
                    var techInspection = await _context.TechInspections.FindAsync(id);
                    if (techInspection == null)
                        return BadRequest("Не возможно удалить несуществующий в базе тех.осмотор");

                    _context.TechInspections.Remove(techInspection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    return BadRequest(e);
                }
                return Ok();
            }
            return BadRequest("На вход поступила не валидная модель");
        }

        // GET: api/CarPark/[action]
        [HttpGet("[action]")]
        public IActionResult GetReport()
        {
            int nowYear = DateTime.Now.Year;
            int totalCars = _context.Cars.Count();
            int totalTechInspection = _context.TechInspections.Count();
            int carsOver3Year = _context.Cars.Count(c => nowYear - c.Year >= 3);

            var report = new
            {
                TotalCars = totalCars,
                TotalTechInspection = totalTechInspection,
                CarsOver3Year = carsOver3Year,
                CarsUnder3Year = totalCars - carsOver3Year
            };

            return new JsonResult(report);
        }
    }
}
