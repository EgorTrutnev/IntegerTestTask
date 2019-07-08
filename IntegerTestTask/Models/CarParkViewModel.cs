using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IntegerTestTask.Models
{
    public class Car
    {
        public int CarId { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [MaxLength(32)]
        public string Model { get; set; }

        [Required]
        [MaxLength(16)]
        public string StateNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RegistrationDate { get; set; }

        public virtual List<TechInspection> TechInspections { get; set; }
    }

    public class TechInspection
    {
        [Key]
        public int InspectionId { get; set; }
        
        [Required]
        public int CarId { get; set; }

        public Car Car { get; set; }

        [Required]
        [MaxLength(64)]
        public string CardNumber { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime DateTechInspection { get; set; }

        public string Comment { get; set; }
    }
}
