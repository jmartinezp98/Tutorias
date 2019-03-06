using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tutorias.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El campo UserName es obligatorio")]
        [Display(Name = "Nombre de Usuario")]
        public String UserName { get; set; }

        [Required(ErrorMessage = "El campo UserPassword es obligatorio")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public String UserPassword { get; set; }
    }
}