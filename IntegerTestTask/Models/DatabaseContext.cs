using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegerTestTask.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) => Database.EnsureCreated();

        public DbSet<Student> Students { get; set; }
        public DbSet<SubjectOfStudy> SubjectsOfStudy { get; set; }
        public DbSet<LearningOutcome> LearningOutcomes { get; set; }

        public DbSet<Car> Cars { get; set; }
        public DbSet<TechInspection> TechInspections { get; set; }
    }
}
