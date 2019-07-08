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
                if (!context.Students.Any() && !context.SubjectsOfStudy.Any() &&
                    !context.LearningOutcomes.Any())
                {
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

                if (!context.Cars.Any() && !context.TechInspections.Any())
                {
                    Car car1 = new Car
                    {
                        Year = 2012,
                        Model = "Toyota Camry",
                        StateNumber = "У101РН750",
                        RegistrationDate = new DateTime(2015, 2, 26)
                    };

                    Car car2 = new Car
                    {
                        Year = 2004,
                        Model = "Renault Clio",
                        StateNumber = "С323РВ69"
                    };

                    Car car3 = new Car
                    {
                        Year = 2011,
                        Model = "Citroen Jumper",
                        StateNumber = "Х989ОР69",
                        RegistrationDate = new DateTime(2013, 3, 2)
                    };

                    Car car4 = new Car
                    {
                        Year = 2011,
                        Model = "Renault Sandero",
                        StateNumber = "В940ОС69",
                        RegistrationDate = new DateTime(2019, 4, 12)
                    };

                    Car car5 = new Car
                    {
                        Year = 2015,
                        Model = "Mercedes Sprinter",
                        StateNumber = "С940СА69",
                        RegistrationDate = new DateTime(2016, 11, 18)
                    };

                    Car car6 = new Car
                    {
                        Year = 2012,
                        Model = "Iveco Daily",
                        StateNumber = "А906ОХ69",
                        RegistrationDate = new DateTime(2017, 5, 23)
                    };

                    Car car7 = new Car
                    {
                        Year = 2017,
                        Model = "Peugeot Boxer",
                        StateNumber = "У329СВ69",
                        RegistrationDate = new DateTime(2019, 2, 11)
                    };

                    Car car8 = new Car
                    {
                        Year = 2011,
                        Model = "Iveco Daily",
                        StateNumber = "Н332ОК69",
                        RegistrationDate = new DateTime(2014, 12, 5)
                    };

                    Car car9 = new Car
                    {
                        Year = 2017,
                        Model = "Peugeot Boxer",
                        StateNumber = "Х659СВ69",
                        RegistrationDate = new DateTime(2018, 2, 28)
                    };

                    Car car10 = new Car
                    {
                        Year = 2018,
                        Model = "Paz 3204",
                        StateNumber = "С584СК69",
                        RegistrationDate = new DateTime(2019, 4, 19)
                    };

                    context.Cars.AddRange(car1, car2, car3, car4, car5,
                                              car6, car7, car8, car9, car10);
                    context.SaveChanges();

                    context.TechInspections.AddRange(
                        new TechInspection { Car = car1, CardNumber= "XgjolP55xopReg", DateTechInspection = new DateTime(2019, 5, 18), Comment = "Правая фара моргает при движении" },
                        new TechInspection { Car = car2, CardNumber = "jKbtbhUnAHTXfo", DateTechInspection = new DateTime(2018, 11, 23), Comment = "Некомплектная аптечка" },
                        new TechInspection { Car = car2, CardNumber = "4oDmfN2VVvBXQ8", DateTechInspection = new DateTime(2019, 5, 19), Comment = "Огнетушитель неисправен" },
                        new TechInspection { Car = car3, CardNumber = "42tf5mNIgkbkgq", DateTechInspection = new DateTime(2019, 5, 14) },
                        new TechInspection { Car = car4, CardNumber = "E59yid0NABSn5X", DateTechInspection = new DateTime(2019, 5, 15) },
                        new TechInspection { Car = car5, CardNumber = "8gQHHqeCFsLRZD", DateTechInspection = new DateTime(2018, 7, 3) },
                        new TechInspection { Car = car5, CardNumber = "2wbW9U2OWMJ754", DateTechInspection = new DateTime(2018, 11, 14), Comment = "Нет огнетушителя" },
                        new TechInspection { Car = car5, CardNumber = "3YaLAxU6cg8o2Q", DateTechInspection = new DateTime(2019, 5, 24) },
                        new TechInspection { Car = car6, CardNumber = "VFtAQuoG5xJiCq", DateTechInspection = new DateTime(2019, 5, 22), Comment = "Неисправные тормоза" },
                        new TechInspection { Car = car7, CardNumber = "mEzqwbgrgLBm07", DateTechInspection = new DateTime(2019, 5, 20), Comment = "Аварийный знак не соответствует ГОСТу" },
                        new TechInspection { Car = car8, CardNumber = "D8cnmEUmGiN3XH", DateTechInspection = new DateTime(2019, 5, 20), Comment = "Нет эффективного торможения" },
                        new TechInspection { Car = car9, CardNumber = "0SSD5bX2iTNu9d", DateTechInspection = new DateTime(2019, 5, 10), Comment = "Нет огнетушителя" });

                    context.SaveChanges();
                }
            }
        }
    }
}
