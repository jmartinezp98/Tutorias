using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tutorias.Models
{
    public class Course
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "El campo Description es obligatorio")]
        [StringLength(35, ErrorMessage = "La longitud es de maximo 50 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El campo Instructor es obligatorio")]
        [StringLength(45, ErrorMessage = "La longitud es de maximo 50 caracteres")]
        public string Instructor { get; set; }
        
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}