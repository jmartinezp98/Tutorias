using Highsoft.Web.Mvc.Charts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tutorias.Models;
using Tutorias.ViewModels;

namespace Tutorias.Controllers
{
    public class StudentsController : Controller
    {
        public static TutoriasDbContext dbCtx = new TutoriasDbContext();

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
                objEstudiante.Matricula = matricula;
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


        public ActionResult RedirectToStudent(string registration)
        {
            if (Session["UserGroup"] != null)
            {
                //se guarda el grupo en una variable
                string group = Session["UserGroup"].ToString();

                //Como la vista del estudiante individual toma como parametro un StudentsViewModel entonces creamos uno para ponerle los datos
                StudentsViewModel student = new StudentsViewModel();

                //se guarda la matricula
                student.Matricula = registration;

                #region SE OBTIENE EL NOMBRE

                //query para obtener el nombre y el id
                var queryStudents = (from s in dbCtx.Students
                                     where s.Registration == registration
                                     select new
                                     {
                                         id = s.ID,
                                         nombre = s.LastNameP + " " + s.LastNameM + " " + s.FirstMidName
                                     }).SingleOrDefault();

                //se agrega al studentsViewModel
                student.Nombre = queryStudents.nombre;

                #endregion

                #region SE OBTIENEN LAS VULNERABILIDADES

                //se realizan 4 queries para obtener el status de cada una de las vulnerabilidades
                var queryVul1 = (from sv in dbCtx.StudentVulnerabilities
                                 join s in dbCtx.Students on sv.StudentID equals s.ID
                                 where sv.StudentID == queryStudents.id && sv.VulnerabilityID == 1
                                 select new
                                 {
                                     vulnerabilities = sv.VulStatus
                                 }).SingleOrDefault();
                var queryVul2 = (from sv in dbCtx.StudentVulnerabilities
                                 join s in dbCtx.Students on sv.StudentID equals s.ID
                                 where sv.StudentID == queryStudents.id && sv.VulnerabilityID == 2
                                 orderby sv.VulnerabilityID
                                 select new
                                 {
                                     vulnerabilities = sv.VulStatus
                                 }).SingleOrDefault();
                var queryVul3 = (from sv in dbCtx.StudentVulnerabilities
                                 join s in dbCtx.Students on sv.StudentID equals s.ID
                                 where sv.StudentID == queryStudents.id && sv.VulnerabilityID == 3
                                 orderby sv.VulnerabilityID
                                 select new
                                 {
                                     vulnerabilities = sv.VulStatus
                                 }).SingleOrDefault();
                var queryVul4 = (from sv in dbCtx.StudentVulnerabilities
                                 join s in dbCtx.Students on sv.StudentID equals s.ID
                                 where sv.StudentID == queryStudents.id && sv.VulnerabilityID == 4
                                 orderby sv.VulnerabilityID
                                 select new
                                 {
                                     vulnerabilities = sv.VulStatus
                                 }).SingleOrDefault();

                //se agregan al StudentsViewModel
                student.Vul1 = queryVul1.vulnerabilities;
                student.Vul2 = queryVul2.vulnerabilities;
                student.Vul3 = queryVul3.vulnerabilities;
                student.Vul4 = queryVul4.vulnerabilities;

                #endregion


                //regresa una vista que lleva el studentsViewModelCreado
                return RedirectToAction("Student", "Students", student);

            }
            else
            {
                //si no se ha hecho login entonces regresa al login
                return RedirectToAction("Login", "Login");
            }
        }


        public ActionResult Course(string materia, string registration)
        {
            //solo si se ha iniciado sesion 
            if(Session["UserGroup"] != null)
            {
                //crear courseViewModel
                CourseViewModel objCourse = new CourseViewModel();

                //la matricula se guarda en un TempData para poder regresar al alumno por medio del redirectToStudent
                TempData["Registration"] = registration;

                #region OBTENER MATERIA

                //Buscar como validar tambien la matricula del alumno por si hay dos materias que se llaman igual pero diferente DIF instructor
                //Query para obtener info de CourseViewModel
                var QuerryMateria = (from c in dbCtx.Courses
                                     where c.Description == materia
                                     select new 
                                     {
                                         id = c.ID,
                                         materia = c.Description,
                                         maestro = c.Instructor
                                     }).SingleOrDefault();
                //variable para guardar id de materia 
                int idMateria = QuerryMateria.id;

                //Poner valores 
                objCourse.Materia = QuerryMateria.materia;
                objCourse.Maestro = QuerryMateria.maestro;

                #endregion

                #region OBTENER UNIDADES

                //se consultan las unidades en la base de datos
                var queryUnidades = (from sc in dbCtx.StudentCourses
                                     join c in dbCtx.Courses on sc.CourseID equals c.ID
                                     join s in dbCtx.Students on sc.StudentID equals s.ID
                                     where c.ID == idMateria && s.Registration == registration
                                     orderby sc.Unit ascending
                                     group sc by sc.Unit into g
                                     select new
                                     {
                                         unidad = g.Key
                                     }).ToList();

                //se crea una lista donde se almacenaran las unidades 
                List<string> unidades = new List<string>();

                foreach (var unit in queryUnidades)
                {
                    //se crea el string que se va a guardar ej. "Unidad 1"
                    string unidad = "Unidad " + unit.unidad.ToString();

                    //se agrega a la lista
                    unidades.Add(unidad);
                }

                //se guarda en un viewData para usarlo en el view
                ViewData["Unidades"] = unidades;

                #endregion

                #region OBTENER COLUMNAS

                //se crea una lista de tipo Column para guardar las columnas
                List<Column> columns = new List<Column>();

                //por cada unidad habrá una columna
                foreach (var unit in queryUnidades)
                {
                    //se crea una columna
                    Column columna = new Column();

                    //query para obtener la calificacion
                    var queryCalificacion = (from sc in dbCtx.StudentCourses
                                             join c in dbCtx.Courses on sc.CourseID equals c.ID
                                             join s in dbCtx.Students on sc.StudentID equals s.ID
                                             //se busca la calificacion de la unidad actual
                                             //que corresponda a la materia y estudiantes actuales
                                             where c.ID == idMateria && s.Registration == registration && sc.Unit == unit.unidad
                                             select new
                                             {
                                                 calificacion = sc.Grade
                                             }).FirstOrDefault();

                    switch (queryCalificacion.calificacion)
                    {
                        case "0":
                            columna.name = "Pendiente";
                            columna.color = "blue";
                            columna.y = null;
                            break;

                        case "NA":
                            columna.name = "No aprobado";
                            columna.color = "#FF0000";
                            columna.y = 0;
                            break;

                        case "8":
                            columna.name = "Suficiente";
                            columna.color = "#FF8000";
                            columna.y = 1;
                            break;

                        case "9":
                            columna.name = "Regular";
                            columna.color = "#FFFF00";
                            columna.y = 2;
                            break;

                        case "10":
                            columna.name = "Excelente";
                            columna.color = "#80FF00";
                            columna.y = 3;
                            break;
                    }

                    //se agrega la columna a la lista
                    columns.Add(columna);
                }

                //Se crea la lista de columnas
                List<ColumnSeriesData> ColumnData = new List<ColumnSeriesData>();

                //se agregan a la lista
                columns.ForEach(p => ColumnData.Add(new ColumnSeriesData { Name = p.name, Color = p.color, Y = p.y }));
                //la lista de columnas se guarda en un viewBag
                ViewData["Columnas"] = ColumnData;

                #endregion

                //Se retorna la vista 
                return View(objCourse);
                
            }
            else
            {
                //si no se ha hecho login entonces regresa al login
                return RedirectToAction("Login","Login");
            }

        }


        [HttpGet]
        public ActionResult Vulnerabilities(StudentViewModel student)
        {
            //solo si se ha iniciado sesion
            if (Session["UserGroup"] != null)
            {
                //se crea un student del viewModel
                VulnerabilitiesViewModel objVulnerabilities = new VulnerabilitiesViewModel();

                //se guarda el nombre y la matricula
                objVulnerabilities.Nombre = student.Nombre;
                objVulnerabilities.Matricula = student.Matricula;

                //se guarda la matricula en una variable
                string registration = student.Matricula;

                #region GUARDAR VULNERABILIDADES YA CONSULTADAS

                //convertimos a string para poder usarlas como radio buttons
                objVulnerabilities.Vul1 = student.Vul1.ToString();
                objVulnerabilities.Vul2 = student.Vul2.ToString();
                objVulnerabilities.Vul3 = student.Vul3.ToString();
                objVulnerabilities.Vul4 = student.Vul4.ToString();

                #endregion

                #region OBTENER COMENTARIOS DE VULNERABILIDADES

                //se obtienen los comentarios de cada una de las vulnerabilidades
                var queryVul1 = (from sv in dbCtx.StudentVulnerabilities
                                 join s in dbCtx.Students on sv.StudentID equals s.ID
                                 where s.Registration == registration && sv.VulnerabilityID == 1
                                 select new
                                 {
                                     coments = sv.VulComments
                                 }).SingleOrDefault();
                var queryVul2 = (from sv in dbCtx.StudentVulnerabilities
                                 join s in dbCtx.Students on sv.StudentID equals s.ID
                                 where s.Registration == registration && sv.VulnerabilityID == 2
                                 orderby sv.VulnerabilityID
                                 select new
                                 {
                                     coments = sv.VulComments
                                 }).SingleOrDefault();
                var queryVul3 = (from sv in dbCtx.StudentVulnerabilities
                                 join s in dbCtx.Students on sv.StudentID equals s.ID
                                 where s.Registration == registration && sv.VulnerabilityID == 3
                                 orderby sv.VulnerabilityID
                                 select new
                                 {
                                     coments = sv.VulComments
                                 }).SingleOrDefault();
                var queryVul4 = (from sv in dbCtx.StudentVulnerabilities
                                 join s in dbCtx.Students on sv.StudentID equals s.ID
                                 where s.Registration == registration && sv.VulnerabilityID == 4
                                 orderby sv.VulnerabilityID
                                 select new
                                 {
                                     coments = sv.VulComments
                                 }).SingleOrDefault();

                //agregar valores de los comentarios
                objVulnerabilities.ComentsVul1 = queryVul1.coments;
                objVulnerabilities.ComentsVul2 = queryVul2.coments;
                objVulnerabilities.ComentsVul3 = queryVul3.coments;
                objVulnerabilities.ComentsVul4 = queryVul4.coments;

                #endregion

                //se retorna la vista
                return View(objVulnerabilities);
            }
            else
            {
                //si no se ha hecho login entonces regresa al login
                return RedirectToAction("Login", "Login");
            }
        }


        [HttpPost]
        public ActionResult Vulnerabilities(VulnerabilitiesViewModel newVulnerabilities)
        {
            //solo si se ha iniciado sesion
            if (Session["UserGroup"] != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        //Guardamos los nuevos valores en variables
                        string registration = newVulnerabilities.Matricula;
                        string nombre = newVulnerabilities.Nombre;
                        int Vul1 = Convert.ToInt16(newVulnerabilities.Vul1);
                        string ComentsVul1 = newVulnerabilities.ComentsVul1;
                        int Vul2 = Convert.ToInt16(newVulnerabilities.Vul2);
                        string ComentsVul2 = newVulnerabilities.ComentsVul2;
                        int Vul3 = Convert.ToInt16(newVulnerabilities.Vul3);
                        string ComentsVul3 = newVulnerabilities.ComentsVul3;
                        int Vul4 = Convert.ToInt16(newVulnerabilities.Vul4);
                        string ComentsVul4 = newVulnerabilities.ComentsVul4;


                        //vamos a tener 4 edits para editar cada vulnerabilidad

                        #region EDITAR VULNERABILIDAD ACADEMICA

                        //Primero buscamos el registro de StudentsVulnerabilities que concuerde con la vulnerabilidad y el estudiante
                        var queryVul1 = (from sv in dbCtx.StudentVulnerabilities
                                         join s in dbCtx.Students on sv.StudentID equals s.ID
                                         where s.Registration == registration && sv.VulnerabilityID == 1
                                         select sv).SingleOrDefault();

                        //se guardan los nuevos valores
                        queryVul1.VulStatus = Vul1;
                        queryVul1.VulComments = ComentsVul1;

                        //se guardan los cambios
                        dbCtx.Entry(queryVul1).State = EntityState.Modified;
                        dbCtx.SaveChanges();

                        #endregion

                        #region EDITAR VULNERABILIDAD ECONOMICA

                        var queryVul2 = (from sv in dbCtx.StudentVulnerabilities
                                         join s in dbCtx.Students on sv.StudentID equals s.ID
                                         where s.Registration == registration && sv.VulnerabilityID == 2
                                         select sv).SingleOrDefault();

                        //se guardan los nuevos valores
                        queryVul2.VulStatus = Vul2;
                        queryVul2.VulComments = ComentsVul2;

                        //se guardan los cambios
                        dbCtx.Entry(queryVul2).State = EntityState.Modified;
                        dbCtx.SaveChanges();

                        #endregion

                        #region EDITAR VULNERABILIDAD PSICOLOGICA

                        var queryVul3 = (from sv in dbCtx.StudentVulnerabilities
                                         join s in dbCtx.Students on sv.StudentID equals s.ID
                                         where s.Registration == registration && sv.VulnerabilityID == 3
                                         select sv).SingleOrDefault();

                        //se guardan los nuevos valores
                        queryVul3.VulStatus = Vul3;
                        queryVul3.VulComments = ComentsVul3;

                        //se guardan los cambios
                        dbCtx.Entry(queryVul3).State = EntityState.Modified;
                        dbCtx.SaveChanges();

                        #endregion

                        #region EDITAR VULNERABILIDAD TRANSPORTE

                        var queryVul4 = (from sv in dbCtx.StudentVulnerabilities
                                         join s in dbCtx.Students on sv.StudentID equals s.ID
                                         where s.Registration == registration && sv.VulnerabilityID == 4
                                         select sv).SingleOrDefault();

                        //se guardan los nuevos valores
                        queryVul4.VulStatus = Vul4;
                        queryVul4.VulComments = ComentsVul4;

                        //se guardan los cambios
                        dbCtx.Entry(queryVul4).State = EntityState.Modified;
                        dbCtx.SaveChanges();

                        #endregion

                        #region LOG

                        //Buscar la carpeta en el proyecto
                        var pathLog = Server.MapPath("~") + @"Files";
                        //nombre del archivo
                        //como aparecia FilesLog.txt se le agregó la diagonal
                        var fileName = "/Log.txt";

                        StreamWriter sw = new StreamWriter(pathLog + fileName, true);
                        //Permite escribir en el archivo .txt
                        sw.WriteLine("Metodo: MainPageVulnerabilitiesEdit -" + DateTime.Now + "- Se editaron las vulnerabilidades del estudiante con matrícula: " + registration);
                        sw.WriteLine("Detalles: MainPageVulnerabilitiesEdit -" + DateTime.Now + "Vulnerabilidad académica status - " + Vul1 + " / Comentarios -" + ComentsVul1);
                        sw.WriteLine("Detalles: MainPageVulnerabilitiesEdit -" + DateTime.Now + "Vulnerabilidad económica status - " + Vul2 + " / Comentarios -" + ComentsVul2);
                        sw.WriteLine("Detalles: MainPageVulnerabilitiesEdit -" + DateTime.Now + "Vulnerabilidad psicológica status - " + Vul3 + " / Comentarios -" + ComentsVul3);
                        sw.WriteLine("Detalles: MainPageVulnerabilitiesEdit -" + DateTime.Now + "Vulnerabilidad transporte status - " + Vul4 + " / Comentarios -" + ComentsVul4);
                        //cierra la conexion
                        sw.Close();

                        #endregion

                        //para regresar a la vista de student se crea un STUDENTSviewModel
                        StudentsViewModel RedirectStudent = new StudentsViewModel();
                        RedirectStudent.Matricula = registration;
                        RedirectStudent.Nombre = nombre;
                        RedirectStudent.Vul1 = Vul1;
                        RedirectStudent.Vul2 = Vul2;
                        RedirectStudent.Vul3 = Vul3;
                        RedirectStudent.Vul4 = Vul4;

                        //se retorna la vista de estudiante
                        return RedirectToAction("Student", "Students", RedirectStudent);
                    }
                    catch
                    {
                        //si no se guardaron los cambios regresa la misma vista
                        return View(newVulnerabilities);
                    }
                }
                //si el modelo no es valido regresa la misma vista
                return View(newVulnerabilities);
            }
            else
            {
                //si no se ha hecho login entonces regresa al login
                return RedirectToAction("Login", "Login");
            }
        }

    }
}

