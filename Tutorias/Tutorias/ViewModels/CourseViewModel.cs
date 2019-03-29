using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Tutorias.ViewModels
{
    public class CourseViewModel
    {
        [Required(ErrorMessage = "El campo Materia es obligatorio")]
        [Display(Name = "Materia")]
        public string Materia { get; set; }

        [Required(ErrorMessage = "El campo Maestro es obligatorio")]
        [Display(Name = "Nombre del maestro")]
        public string Maestro { get; set; }
    }
}