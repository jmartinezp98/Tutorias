using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Tutorias.ViewModels
{
    public class StudentViewModel
    {
        [Required(ErrorMessage = "El campo Registration es obligatorio")]
        [Display(Name = "Matricula del alumno")]
        public string Registration { get; set; }

        [Required(ErrorMessage = "El campo FirstMidName es obligatorio")]
        [Display(Name = "Nombre")]
        public string FirstMidName { get; set; }

        [Required(ErrorMessage = "El campo LastNameP es obligatorio")]
        [Display(Name = "Apellido paterno")]
        public string LastNameP { get; set; }

        [Required(ErrorMessage = "El campo LastNameM es obligatorio")]
        [Display(Name = "Apellido Materno")]
        public string LastNameM { get; set; }

        [Required(ErrorMessage = "El campo BirthDate es obligatorio")]
        [Display(Name = "Fecha de nacimiento")]
        public string BirthDate { get; set; }

        [Required(ErrorMessage = "El campo CURP es obligatorio")]
        [Display(Name = "CURP")]
        public string CURP { get; set; }

        [Required(ErrorMessage = "El campo RFC es obligatorio")]
        [Display(Name = "RFC")]
        public string RFC { get; set; }

        [Required(ErrorMessage = "El campo MaritalStatus es obligatorio")]
        [Display(Name = "Estado Civil")]
        public string MaritalStatus { get; set; }

        [Required(ErrorMessage = "El campo PersonalPhone es obligatorio")]
        [Display(Name = "Telefono personal")]
        public string PersonalPhone { get; set; }

        [Required(ErrorMessage = "El campo EmergencyPhone es obligatorio")]
        [Display(Name = "Telefono de emergencia")]
        public string EmergencyPhone { get; set; }

        [Required(ErrorMessage = "El campo PersonalEmail es obligatorio")]
        [Display(Name = "Email personal")]
        public string PersonalEmail { get; set; }

        [Required(ErrorMessage = "El campo AcademicEmail es obligatorio")]
        [Display(Name = "Email academico")]
        public string AcademicEmail { get; set; }

        [Required(ErrorMessage = "El campo Career es obligatorio")]
        [Display(Name = "Carrera")]
        public string Career { get; set; }

        [Required(ErrorMessage = "El campo Modality es obligatorio")]
        [Display(Name = "Modalidad")]
        public string Modality { get; set; }

        [Required(ErrorMessage = "El campo Term es obligatorio")]
        [Display(Name = "Cuatrimestre")]
        public int Term { get; set; }

        [Required(ErrorMessage = "El campo Section es obligatorio")]
        [Display(Name = "Sección")]
        public string Section { get; set; }

        [Required(ErrorMessage = "El campo Turn es obligatorio")]
        [Display(Name = "Turno")]
        public string Turn { get; set; }

        [Required(ErrorMessage = "El campo Situation es obligatorio")]
        [Display(Name = "Situación")]
        public string Situation { get; set; }

        [Display(Name = "Academica")]
        public int Vul1 { get; set; }

        [Display(Name = "Economica")]
        public int Vul2 { get; set; }

        [Display(Name = "Psicologica")]
        public int Vul3 { get; set; }

        [Display(Name = "Transporte")]
        public int Vul4 { get; set; }

        [Required(ErrorMessage = "El campo PhotoPath es obligatorio")]
        [Display(Name = "Foto")]
        public string PhotoPath { get; set; }
    }
}