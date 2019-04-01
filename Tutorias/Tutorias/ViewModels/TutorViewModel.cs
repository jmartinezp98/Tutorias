using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tutorias.ViewModels
{
    public class TutorViewModel
    {
        [Required(ErrorMessage = "El campo EmployeeID es obligatorio")]
        [Display(Name = "Matricula de empleado")]
        public string EmployeeID { get; set; }

        [Required(ErrorMessage = "The field Nombre is required")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

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

        [Required(ErrorMessage = "El campo MaritalStatusID es obligatorio")]
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

        [Required(ErrorMessage = "El campo PhotoPath es obligatorio")]
        [Display(Name = "Foto")]
        public string PhotoPath { get; set; }

    }
}