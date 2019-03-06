using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace Tutorias.Models
{
    public class Career
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "El campo Description es obligatorio")]
        [StringLength(80, ErrorMessage = "La longitud es de maximo 50 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El campo Building es obligatorio")]
        [StringLength(10, ErrorMessage = "La longitud es de maximo 10 caracteres")]  
        public string Building { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}