using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tutorias.ViewModels
{
    public class VulnerabilitiesViewModel
    {
        [Required(ErrorMessage = "The field Matricula is required")]
        [Display(Name = "Matrícula")]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "The field Nombre is required")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Academica")]
        public string Vul1 { get; set; }

        [Display(Name = "Economica")]
        public string Vul2 { get; set; }

        [Display(Name = "Psicologica")]
        public string Vul3 { get; set; }

        [Display(Name = "Transporte")]
        public string Vul4 { get; set; }

        [Display(Name = "Comentarios Academica")]
        public string ComentsVul1 { get; set; }

        [Display(Name = "Comentarios Economica")]
        public string ComentsVul2 { get; set; }

        [Display(Name = "Comentarios Psicologica")]
        public string ComentsVul3 { get; set; }

        [Display(Name = "Comentarios Transporte")]
        public string ComentsVul4 { get; set; }
    }
}