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

        //Vamos a obtener como parametros la matricula del estudante selecionado 
        public ActionResult Student(StudentsViewModel student)
        {
            //Se valida si se ha iniciado sesion
            if(Session["UserGroup"]!= null)
            {


                //Se crea un student del viewModel
                StudentViewModel objEstudiante = new StudentViewModel();

                //Se guarda el nombre y la matricula 
                objEstudiante.Nombre = student.Nombre;
                objEstudiante.Matricula = student.Matricula;

                //Se guarda la matricula en una variable
                string matricula = student.Matricula;


                #region OBTENER FOTO DE ESTUDIANTE EN BASE A MATRICULA

                //como tenemos las fotos guardadas con matricula no necesitamos hacer un query
                //a la matricula se le agrega el .jpg
                string path = matricula + ".jpg";

                //se agrega al ViewModel
                objEstudiante.Registration = matricula;
                objEstudiante.PhotoPath = path;

                #endregion

                #region OBTENER DATOS ESTUDIANTE EN BASE A MATRICULA

                //se obtiene la informacion del alumno
                var queryStudent = (from s in dbCtx.Students
                                    join cg in dbCtx.ClassGroups on s.ClassGroupID equals cg.ID
                                    join ms in dbCtx.MaritalStatuses on s.MaritalStatusID equals ms.ID
                                    join c in dbCtx.Careers on s.CareerID equals c.ID
                                    join m in dbCtx.Modalities on s.ModalityID equals m.ID
                                    join tu in dbCtx.Turns on s.TurnID equals tu.ID
                                    join si in dbCtx.Situations on s.SituationID equals si.ID
                                    where s.Registration == matricula
                                    select new
                                    {
                                        nombre = s.FirstMidName,
                                        apellidoP = s.LastNameP,
                                        apellidoM = s.LastNameM,
                                        birthDate = s.BirthDate,
                                        curp = s.CURP,
                                        rfc = s.RFC,
                                        maritalStatus = ms.Description,
                                        perPhone = s.PersonalPhone,
                                        perEmail = s.PersonalEmail,
                                        emerPhone = s.EmergencyPhone,
                                        academEmail = s.AcademicEmail,
                                        carrera = c.Description,
                                        modalidad = m.Description,
                                        turno = tu.Description,
                                        cuatrimestre = cg.Term,
                                        situacion = si.Description,
                                        seccion = cg.Section
                                    }).SingleOrDefault();

                //se le agregan los valores
                objEstudiante.FirstMidName = queryStudent.nombre;
                objEstudiante.LastNameP = queryStudent.apellidoP;
                objEstudiante.LastNameM = queryStudent.apellidoM;
                objEstudiante.BirthDate = queryStudent.birthDate;
                objEstudiante.CURP = queryStudent.curp;
                objEstudiante.RFC = queryStudent.rfc;
                objEstudiante.MaritalStatus = queryStudent.maritalStatus;
                objEstudiante.PersonalPhone = queryStudent.perPhone;
                objEstudiante.PersonalEmail = queryStudent.perEmail;
                objEstudiante.EmergencyPhone = queryStudent.emerPhone;
                objEstudiante.AcademicEmail = queryStudent.academEmail;
                objEstudiante.Career = queryStudent.carrera;
                objEstudiante.Modality = queryStudent.modalidad;
                objEstudiante.Turn = queryStudent.turno;
                objEstudiante.Term = queryStudent.cuatrimestre;
                objEstudiante.Section = queryStudent.seccion;
                objEstudiante.Situation = queryStudent.situacion;

                //se agregan al objEstudiante las vulnerabilidades del parametro student
                objEstudiante.Vul1 = student.Vul1;
                objEstudiante.Vul2 = student.Vul2;
                objEstudiante.Vul3 = student.Vul3;
                objEstudiante.Vul4 = student.Vul4;


                #endregion

                #region OBTENER MATERIAS 
                // se buscan las materias que lleva el alumno
                var queryMaterias = (from sc in dbCtx.StudentCourses
                                     join c in dbCtx.Courses on sc.CourseID equals c.ID
                                     join s in dbCtx.Students on sc.StudentID equals s.ID
                                     where s.Registration == matricula
                                     //para obtener solo una vez la materia y no por cada unidad
                                     group sc by c.Description into g
                                     select new
                                     {
                                         materia = g.Key
                                     }).ToList();


                //lista para almacenar las materias
                List<string> materias = new List<string>();

                //por cada materia se agrega un elemento a la lista
                foreach (var materia in queryMaterias)
                {
                    materias.Add(materia.materia);
                }

                //se guarda la lista en un view bag
                ViewBag.Materias = materias;

                #endregion

                //Se retorna la vista con el estudiante de StudentViewModel
                return View(objEstudiante);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }



    }
}
