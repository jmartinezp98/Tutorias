using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tutorias.Models
{
    public class MaritalStatus
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "El campo Description es obligatorio")]
        [StringLength(35, ErrorMessage = "La longitud para el campo Description es de maximo 35 caracteres")]
        public string Description { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Tutor> Tutors { get; set; }
    }
}