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

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Student>()
        //        .HasMany(l => l.LearningOutcomes);
        //    modelBuilder.Entity<SubjectOfStudy>()
        //        .HasMany(l => l.LearningOutcomes);
        //}
    }
}
