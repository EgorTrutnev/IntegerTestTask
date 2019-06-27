using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegerTestTask.Models;
using IntegerTestTask.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntegerTestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly DatabaseContext _context;

        public StudentsController(DatabaseContext context) =>
            _context = context;

        private class StudentsList<T>
        {
            public T studentId { get; set; }
            public T name { get; set; }
            public T dateOfBirthday { get; set; }
            public T studentYear { get; set; }
        }

        // GET: api/Server
        [HttpGet("[action]")]
        public async Task<IActionResult> GetStudents()
        {
            List<StudentsList<dynamic>> studentslist = new List<StudentsList<dynamic>>();
            foreach (var student in await _context.Students.ToListAsync())
                studentslist.Add(new StudentsList<dynamic>()
                {
                    studentId = student.StudentId,
                    name = student.Name,
                    dateOfBirthday = student.DateOfBirthday.ToShortDateString(),
                    studentYear = student.StudentYear
                });

            return Ok(studentslist);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSubjects()
        {
            return Ok(await _context.SubjectsOfStudy.ToListAsync());
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetLearns()
        {
            return Ok(await _context.LearningOutcomes.ToListAsync());
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSample(int param)
        {
            if (param == 1)
            {
                DateTime USSRBreakupDate = new DateTime(1991, 4, 26);

                var result = _context.Students
                    .Where(u => u.DateOfBirthday < USSRBreakupDate)
                    .Select(u => u.StudentId)
                    .SelectMany(e => _context.LearningOutcomes
                        .Where(l => l.SubjectId == _context.SubjectsOfStudy
                            .Where(s => s.Title == "Информатика")
                            .Select(s => s.SubjectId)
                            .First() && l.Score >= 3)
                        .Where(l => l.StudentId == e)
                        .Select(fl => new
                        {
                            id = fl.Id,
                            studentId = fl.StudentId,
                            subject = fl.SubjectId,
                            score = fl.Score
                        }));

                return Ok(await result.ToListAsync());
            }
            else if(param == 2)
            {
                var result = _context.LearningOutcomes
                    .GroupBy(l => l.StudentId, g => g.Score)
                    .Select(l =>
                    new {
                        StudentId = l.Key,
                        AverageScore = l.Average()
                    })
                    .Where(g => g.AverageScore == 5);

                return Ok(await result.ToListAsync());
            }
            else if (param == 3)
            {
                var result = _context.LearningOutcomes
                    .GroupBy(l => l.StudentId, g => g.Score)
                    .Select(l =>
                    new {
                        StudentId = l.Key,
                        SumScore = l.Sum()
                    })
                    .OrderByDescending(o => o.SumScore);

                return Ok(await result.ToListAsync());
            }
            else if (param == 4)
            {
                var result = _context.Students
                    .GroupBy(s => s.StudentYear)
                    .Select(l =>
                    new {
                        StudentYear = l.Key,
                        StudentsCount = l.Count()
                    })
                    .OrderBy(c => c.StudentYear);

                return Ok(await result.ToListAsync());
            }
            else if (param == 5)
            {
                var result = _context.Students.Select(l =>
                    new
                    {
                        l.StudentId,
                        SumBirth = getSumDateOfBirthday(l.DateOfBirthday)
                    })
                    .Where(l => l.SumBirth < 40);

                return Ok(await result.ToListAsync());
            }
            else if (param == 6)
            {
                var result = _context.Students.Select(l => l)
                    .Where(l => l.StudentYear == 6);

                return Ok(await result.ToListAsync());
            }
            return Json(new Error(2016, $"Сортировки с номером параметра ('param' = {param}) не существует в текущей выборке."));
        }

        int getSumDateOfBirthday (DateTime dateTime) =>
            dateTime.ToShortDateString().Replace(".", "")
                .Select(x => (int)char.GetNumericValue(x)).ToArray()
                .Aggregate((x, y) => x + y);
        
    }
}
