using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tutorias.Models
{
    public class StudentCourse
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Student")]
        public int StudentID { get; set; }
        public Student Student { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public Course Course { get; set; }

        [Key, Column(Order = 2)]
        [Required(ErrorMessage = "The field Unit is required")]
        [Range(0, int.MaxValue, ErrorMessage = "El campo Unit debe ser un numero entero")]
        public int Unit { get; set; }

        [Required(ErrorMessage = "The field Grade is required")]
        [StringLength(4, ErrorMessage = "La longitud para el campo Grade es de maximo 4 caracteres")]
        public string Grade { get; set; }

    }
}