using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Tutorias.ViewModels
{
    public class StudentsViewModel
    {
        [Required(ErrorMessage = "The field Matricula is required")]
        [Display(Name = "Matrícula")]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "The field Nombre is required")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Academica")]
        public int Vul1 { get; set; }

        [Display(Name = "Economica")]
        public int Vul2 { get; set; }

        [Display(Name = "Psicologica")]
        public int Vul3 { get; set; }

        [Display(Name = "Transporte")]
        public int Vul4 { get; set; }
    }
}