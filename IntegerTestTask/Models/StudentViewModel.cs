using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IntegerTestTask.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(64)]
        public string Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfBirthday { get; set; }

        [MaxLength(1)]
        public byte StudentYear { get; set; }

        public virtual List<LearningOutcome> LearningOutcomes { get; set; }
    }

    public class SubjectOfStudy
    {
        [Key]
        public int SubjectId { get; set; }

        [MaxLength(32)]
        public string Title { get; set; }

        public virtual List<LearningOutcome> LearningOutcomes { get; set; }
    }

    public class LearningOutcome
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        [Required]
        public Student Student { get; set; }

        public int SubjectId { get; set; }

        [Required]
        public SubjectOfStudy Subject { get; set; }

        [MaxLength(1)]
        public int Score { get; set; }
    }
}
