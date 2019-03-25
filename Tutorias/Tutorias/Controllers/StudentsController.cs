using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tutorias.Models;
using Tutorias.ViewModels;

namespace Tutorias.Controllers
{
    public class StudentsController : Controller
    {
        public static TutoriasDbContext dbCtx = new TutoriasDbContext();

        // GET: Students
        [HttpGet]
        public ActionResult Students()
        {
            //se valida si se ha iniciado sesion
            if (Session["UserGroup"] != null)
            {
                //se guarda el grupo en una variable
                string group = Session["UserGroup"].ToString();

                #region OBTENER ESTUDIANTES
                //se crea una lista que guardara los estudiantes
                List<StudentsViewModel> students = new List<StudentsViewModel>();

                //query que obtiene todos los estudiantes pertenecientes al grupo
                //se obtienen solo los campos para el view model
                var queryStudents = (from s in dbCtx.Students
                                     join cg in dbCtx.ClassGroups on s.ClassGroupID equals cg.ID
                                     where @group == cg.GroupID
                                     orderby s.LastNameP ascending
                                     select new
                                     {
                                         id = s.ID,
                                         matricula = s.Registration,
                                         nombre = s.FirstMidName,
                                         apellidoP = s.LastNameP,
                                         apellidoM = s.LastNameM
                                     }
                                     ).ToList();

                //por cada estudiante del grupo
                foreach (var student in queryStudents)
                {
                    //se realizan 4 queries para obtener el status de cada una de las vulnerabilidades
                    var queryVul1 = (from sv in dbCtx.StudentVulnerabilities
                                     join s in dbCtx.Students on sv.StudentID equals s.ID
                                     where sv.StudentID == student.id && sv.VulnerabilityID == 1
                                     select new
                                     {
                                         vulnerabilities = sv.VulStatus
                                     }).SingleOrDefault();
                    var queryVul2 = (from sv in dbCtx.StudentVulnerabilities
                                     join s in dbCtx.Students on sv.StudentID equals s.ID
                                     where sv.StudentID == student.id && sv.VulnerabilityID == 2
                                     orderby sv.VulnerabilityID
                                     select new
                                     {
                                         vulnerabilities = sv.VulStatus
                                     }).SingleOrDefault();
                    var queryVul3 = (from sv in dbCtx.StudentVulnerabilities
                                     join s in dbCtx.Students on sv.StudentID equals s.ID
                                     where sv.StudentID == student.id && sv.VulnerabilityID == 3
                                     orderby sv.VulnerabilityID
                                     select new
                                     {
                                         vulnerabilities = sv.VulStatus
                                     }).SingleOrDefault();
                    var queryVul4 = (from sv in dbCtx.StudentVulnerabilities
                                     join s in dbCtx.Students on sv.StudentID equals s.ID
                                     where sv.StudentID == student.id && sv.VulnerabilityID == 4
                                     orderby sv.VulnerabilityID
                                     select new
                                     {
                                         vulnerabilities = sv.VulStatus
                                     }).SingleOrDefault();

                    //se crea un nuevo estudiante en base al view model
                    StudentsViewModel objEstudiante = new StudentsViewModel();

                    //se le asignan los valores que se obtuvieron en el query
                    objEstudiante.Matricula = student.matricula;
                    objEstudiante.Nombre = student.apellidoP + " " + student.apellidoM + " " + student.nombre;

                    //se asigna el estatus de cada vulnerabilidad
                    objEstudiante.Vul1 = queryVul1.vulnerabilities;
                    objEstudiante.Vul2 = queryVul2.vulnerabilities;
                    objEstudiante.Vul3 = queryVul3.vulnerabilities;
                    objEstudiante.Vul4 = queryVul4.vulnerabilities;

                    //se agrega el estudiante a la lista
                    students.Add(objEstudiante);

                }

                #endregion

                //regresa una vista que lleva la lista de estudiantes
                return View(students);

            }
            else
            {
                //si no se ha hecho login entonces regresa al login
                return RedirectToAction("Login", "Login");
            }
        }
    }
}
