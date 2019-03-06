using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tutorias.Models
{
    public class ClassGroup
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "El campo GroupID es obligatorio")]
        [StringLength(35, ErrorMessage = "La longitud es de maximo 35 caracteres")]
        public string GroupID { get; set; }

        [Required(ErrorMessage = "El campo Term es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El campo Term debe ser un numero entero")]
        public int Term { get; set; }

        [Required(ErrorMessage = "El campo Section es obligatorio")]
        [StringLength(2, ErrorMessage = "La longitud es de maximo 2 caracteres")]
        public string Section { get; set; }

        [ForeignKey("Tutor")]
        [Required(ErrorMessage = "El campo Tutor es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El campo TutorID debe ser un numero entero")]
        public int TutorID { get; set; }
        public Tutor Tutor { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}