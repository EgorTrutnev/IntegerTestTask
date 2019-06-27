using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegerTestTask.Models
{
    public class DatabaseSeedContext
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var context = new DatabaseContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DatabaseContext>>()))
            {
                if (context.Students.Any() && context.SubjectsOfStudy.Any() &&
                    context.LearningOutcomes.Any())
                    return;

                Student student1 = new Student
                {
                    Name = "Иванов И.И.",
                    DateOfBirthday = new DateTime(1989, 1, 2),
                    StudentYear = 5
                };

                Student student2 = new Student
                {
                    Name = "Петров П.П.",
                    DateOfBirthday = new DateTime(1991, 5, 13),
                    StudentYear = 4
                };


                Student student3 = new Student
                {
                    Name = "Сидоров В.В.",
                    DateOfBirthday = new DateTime(1990, 8, 14),
                    StudentYear = 4
                };

                Student student4 = new Student
                {
                    Name = "Коршунов П.И.",
                    DateOfBirthday = new DateTime(1988, 12, 12),
                    StudentYear = 5
                };

                Student student5 = new Student
                {
                    Name = "Артёмов П.Р.",
                    DateOfBirthday = new DateTime(1996, 4, 1),
                    StudentYear = 2
                };

                Student student6 = new Student
                {
                    Name = "Соколов Д.И.",
                    DateOfBirthday = new DateTime(1997, 2, 21),
                    StudentYear = 1
                };

                Student student7 = new Student
                {
                    Name = "Внуков К.Е.",
                    DateOfBirthday = new DateTime(1987, 9, 18),
                    StudentYear = 5
                };

                Student student8 = new Student
                {
                    Name = "Климов А.А.",
                    DateOfBirthday = new DateTime(1997, 7, 11),
                    StudentYear = 1
                };

                Student student9 = new Student
                {
                    Name = "Алексеенко И.А.",
                    DateOfBirthday = new DateTime(1999, 5, 29),
                    StudentYear = 1
                };

                Student student10 = new Student
                {
                    Name = "Дедов И.Л.",
                    DateOfBirthday = new DateTime(1992, 11, 4),
                    StudentYear = 3
                };

                context.Students.AddRange(student1, student2, student3, student4, student5,
                                          student6, student7, student8, student9, student10);
                context.SaveChanges();

                SubjectOfStudy subjectOfStudy1 = new SubjectOfStudy { Title = "Информатика" };
                SubjectOfStudy subjectOfStudy2 = new SubjectOfStudy { Title = "Экономика" };

                context.SubjectsOfStudy.AddRange(subjectOfStudy1, subjectOfStudy2);
                context.SaveChanges();

                context.LearningOutcomes.AddRange(
                    new LearningOutcome { Student = student1, Subject = subjectOfStudy1, Score = 4 },
                    new LearningOutcome { Student = student2, Subject = subjectOfStudy1, Score = 3 },
                    new LearningOutcome { Student = student3, Subject = subjectOfStudy1, Score = 5 },
                    new LearningOutcome { Student = student4, Subject = subjectOfStudy1, Score = 2 },
                    new LearningOutcome { Student = student5, Subject = subjectOfStudy1, Score = 2 },
                    new LearningOutcome { Student = student6, Subject = subjectOfStudy1, Score = 5 },
                    new LearningOutcome { Student = student7, Subject = subjectOfStudy1, Score = 3 },
                    new LearningOutcome { Student = student8, Subject = subjectOfStudy1, Score = 4 },
                    new LearningOutcome { Student = student9, Subject = subjectOfStudy1, Score = 5 },
                    new LearningOutcome { Student = student10, Subject = subjectOfStudy1, Score = 4 },
                    new LearningOutcome { Student = student1, Subject = subjectOfStudy2, Score = 2 },
                    new LearningOutcome { Student = student2, Subject = subjectOfStudy2, Score = 3 },
                    new LearningOutcome { Student = student3, Subject = subjectOfStudy2, Score = 5 },
                    new LearningOutcome { Student = student4, Subject = subjectOfStudy2, Score = 4 },
                    new LearningOutcome { Student = student5, Subject = subjectOfStudy2, Score = 5 },
                    new LearningOutcome { Student = student6, Subject = subjectOfStudy2, Score = 3 },
                    new LearningOutcome { Student = student7, Subject = subjectOfStudy2, Score = 4 },
                    new LearningOutcome { Student = student8, Subject = subjectOfStudy2, Score = 4 },
                    new LearningOutcome { Student = student9, Subject = subjectOfStudy2, Score = 5 },
                    new LearningOutcome { Student = student10, Subject = subjectOfStudy2, Score = 4 });

                context.SaveChanges();
            }
        }
    }
}
