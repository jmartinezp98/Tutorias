using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutorias.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "The field Registration is required")]
        [StringLength(10, ErrorMessage = "The maximum length is 10 characters")]
        public string Registration { get; set; }

        [Required(ErrorMessage = "The field FirstMidName is required")]
        [StringLength(25, ErrorMessage = "The maximum length is 25 characters")]
        public string FirstMidName { get; set; }

        [Required(ErrorMessage = "The field LastNameP is required")]
        [StringLength(20, ErrorMessage = "The maximum length is 20 characters")]
        public string LastNameP { get; set; }

        [Required(ErrorMessage = "The field LastNameM is required")]
        [StringLength(20, ErrorMessage = "The maximum length is 20 characters")]
        public string LastNameM { get; set; }

        [Required(ErrorMessage = "The field BirthDate is required")]
        [StringLength(20, ErrorMessage = "The maximum length is 20 characters")]
        public string BirthDate { get; set; }

        [Required(ErrorMessage = "The field CURP is required")]
        [StringLength(18, ErrorMessage = "The maximum length is 18 characters")]
        public string CURP { get; set; }

        [Required(ErrorMessage = "The field RFC is required")]
        [StringLength(14, ErrorMessage = "The maximum length is 14 characters")]
        public string RFC { get; set; }

        [ForeignKey("MaritalStatus")]
        [Required(ErrorMessage = "The field MaritalStatus is required")]
        [Range(0, int.MaxValue, ErrorMessage = "The field MaritalStatus must be an integer number")]
        public int MaritalStatusID { get; set; }
        public MaritalStatus MaritalStatus { get; set; }

        [StringLength(15, ErrorMessage = "The maximum length is 15 characters")]
        public string PersonalPhone { get; set; }

        [Required(ErrorMessage = "The field EmergencyPhone is required")]
        [StringLength(15, ErrorMessage = "The maximum length is 15 characters")]
        public string EmergencyPhone { get; set; }

        [StringLength(40, ErrorMessage = "The maximum length is 40 characters")]
        public string PersonalEmail { get; set; }

        [Required(ErrorMessage = "The field AcademicEmail is required")]
        [StringLength(30, ErrorMessage = "The maximum length is 20 characters")]
        public string AcademicEmail { get; set; }

        [ForeignKey("Career")]
        [Required(ErrorMessage = "The field Career is required")]
        [Range(0, int.MaxValue, ErrorMessage = "The field Carrer must be an integer number")]
        public int CareerID { get; set; }
        public Career Career { get; set; }

        [ForeignKey("Modality")]
        [Required(ErrorMessage = "The field Modality is required")]
        [Range(0, int.MaxValue, ErrorMessage = "The field Modality must be an integer number")]
        public int ModalityID { get; set; }
        public Modality Modality { get; set; }

        [ForeignKey("Turn")]
        [Required(ErrorMessage = "The field Turn is required")]
        [Range(0, int.MaxValue, ErrorMessage = "The field Turn must be an integer number")]
        public int TurnID { get; set;}
        public Turn Turn { get; set; }

        [ForeignKey("ClassGroup")]
        [Required(ErrorMessage = "The field ClassGroup is required")]
        [Range(0, int.MaxValue, ErrorMessage = "The field ClassGroup must be an integer number")]
        public int ClassGroupID  { get; set;}
        public ClassGroup ClassGroup { get; set; }

        [ForeignKey("Situation")]
        [Required(ErrorMessage = "The field Situation is required")]
        [Range(0, int.MaxValue, ErrorMessage = "The field Situation must be an integer number")]
        public int SituationID { get; set;}
        public Situation Situation { get; set; }

        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
        public virtual ICollection<StudentVulnerability> StudentVulnerabilities { get; set; }
    }
}