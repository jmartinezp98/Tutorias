using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutorias.Models
{
    public class Tutor
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "El campo EmployeeID es obligatorio")]
        [StringLength(8, ErrorMessage = "La longitud es de 8 caracteres")]
        public string EmployeeID { get; set; }

        [Required(ErrorMessage = "El campo FirstMidName es obligatorio")]
        [StringLength(25, ErrorMessage = "La longitud es de 25 caracteres")]
        public string FirstMidName { get; set; }

        [Required(ErrorMessage = "El campo LastNameP es obligatorio")]
        [StringLength(20, ErrorMessage = "La longitud es de 20 caracteres")]
        public string LastNameP { get; set; }

        [Required(ErrorMessage = "El campo LastNameM es obligatorio")]
        [StringLength(20, ErrorMessage = "La longitud es de 20 caracteres")]
        public string LastNameM { get; set; }

        [Required(ErrorMessage = "El campo UserName es obligatorio")]
        [StringLength(15, ErrorMessage = "La longitud es de 15 caracteres")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El campo UserPassword es obligatorio")]
        [StringLength(15, ErrorMessage = "La longitud es de 15 caracteres")]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "El campo BirthDate es obligatorio")]
        [StringLength(20, ErrorMessage = "La longitud es de 20 caracteres")]
        public string BirthDate { get; set; }

        [Required(ErrorMessage = "El campo CURP es obligatorio")]
        [StringLength(18, ErrorMessage = "La longitud es de 18 caracteres")]
        public string CURP { get; set; }

        [Required(ErrorMessage = "El campo RFC es obligatorio")]
        [StringLength(14, ErrorMessage = "La longitud es de 14 caracteres")]
        public string RFC { get; set; }

        [ForeignKey("MaritalStatus")]
        [Required(ErrorMessage = "El campo MaritalStatusID es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Num")]
        public int MaritalStatusID { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        
        [StringLength(15, ErrorMessage = "La longitud es de 15 caracteres")]
        public string PersonalPhone { get; set; }

        [Required(ErrorMessage = "El campo EmergencyPhone es obligatorio")]
        [StringLength(15, ErrorMessage = "La longitud es de 15 caracteres")]
        public string EmergencyPhone { get; set; }
        
        [StringLength(30, ErrorMessage = "La longitud es de 20 caracteres")]
        public string PersonalEmail { get; set; }

        [Required(ErrorMessage = "El campo AcademicEmail es obligatorio")]
        [StringLength(30, ErrorMessage = "La longitud es de 20 caracteres")]
        public string AcademicEmail { get; set; }

        public virtual ICollection<ClassGroup> ClassGroups { get; set; }
    }
}