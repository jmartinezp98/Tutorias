using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tutorias.Models;
using Tutorias.ViewModels;

namespace Tutorias.Controllers
{
    public class MainPageController : Controller
    {
        public static TutoriasDbContext dbCtx = new TutoriasDbContext();

        // GET: MainPage
        public ActionResult MainPage()
        {
            //a esta pagina solo se puede acceder si ha iniciado sesion
            if (Session["UserGroup"] != null)
            {
                //se guarda el grupo en una variable para luego usarla en los query
                string group = Session["UserGroup"].ToString();

                //se crea un tutor del viewModel
                TutorViewModel objTutor = new TutorViewModel();

                #region OBTENER FOTO DEL TUTOR
                //query que en realidad obtiene el CURP del tutor
                var queryFoto = (from t in dbCtx.Tutors
                                 join cg in dbCtx.ClassGroups on t.ID equals cg.TutorID
                                 where @group == cg.GroupID
                                 select new
                                 {
                                     CURP = t.CURP
                                 }).SingleOrDefault();

                //al CURP se le agrega el .jpg
                string path = queryFoto.CURP.ToString() + ".JPEG";

                //se agrega al tutor
                objTutor.PhotoPath = path;
                #endregion

                #region OBTENER INFORMACION DEL TUTOR
                //se obtiene la informacion necesaria del tutor
                var queryTutor = (from t in dbCtx.Tutors
                                  join cg in dbCtx.ClassGroups on t.ID equals cg.TutorID
                                  join ms in dbCtx.MaritalStatuses on t.MaritalStatusID equals ms.ID
                                  where @group == cg.GroupID
                                  select new
                                  {
                                      idEmpleado = t.EmployeeID,
                                      nombre = t.FirstMidName,
                                      apellidoP = t.LastNameP,
                                      apellidoM = t.LastNameM,
                                      birthDate = t.BirthDate,
                                      curp = t.CURP,
                                      rfc = t.RFC,
                                      maritalStatus = ms.Description,
                                      perPhone = t.PersonalPhone,
                                      perEmail = t.PersonalEmail,
                                      emerPhone = t.EmergencyPhone,
                                      academEmail = t.AcademicEmail
                                  }).SingleOrDefault();

                //se agrega al tutor
                objTutor.EmployeeID = queryTutor.idEmpleado;
                objTutor.FirstMidName = queryTutor.nombre;
                objTutor.LastNameP = queryTutor.apellidoP;
                objTutor.LastNameM = queryTutor.apellidoM;
                objTutor.BirthDate = queryTutor.birthDate;
                objTutor.CURP = queryTutor.curp;
                objTutor.RFC = queryTutor.rfc;
                objTutor.MaritalStatus = queryTutor.maritalStatus;
                objTutor.PersonalPhone = queryTutor.perPhone;
                objTutor.EmergencyPhone = queryTutor.emerPhone;
                objTutor.PersonalEmail = queryTutor.perEmail;
                objTutor.AcademicEmail = queryTutor.academEmail;

                #endregion

                #region OBTENER GRAFICA
                
                //se va a obtener el estudiante y la cantidad de vulnerabilidades que tiene
                var queryVulnerables = (from s in dbCtx.Students
                                        join sv in dbCtx.StudentVulnerabilities on s.ID equals sv.StudentID
                                        join cg in dbCtx.ClassGroups on s.ClassGroupID equals cg.ID
                                        where @group == cg.GroupID
                                        group sv by s.ID into g
                                        select new
                                        {
                                            student = g.Key,
                                            //como se agrupa por estudiante va a sumar sus vulnerabilidades
                                            vulnerable = g.Sum(x => x.VulStatus)
                                        }).ToList();

                //Recorrer el resultado de la consulta anteriormente realizada 
                //separar vulnerables y no vulnerables
                int vulnerables = 0;
                int noVulnerables = 0;
                foreach (var item in queryVulnerables)
                {
                    //si el alumno tiene una o mas vulnerabilidades entonces se cuenta en los vulnerables
                    if (item.vulnerable > 0)
                    {
                        vulnerables++;
                    }
                    //si el alumno no tiene vulnerabilidades entonces se cuenta en los no vulnerables
                    if (item.vulnerable == 0)
                    {
                        noVulnerables++;
                    }
                }
                
                //guardamos los resultados en viewData para usarlos en la vista
                ViewData["Vulnerables"] = vulnerables;
                ViewData["NoVulnerables"] = noVulnerables;

                //para el drilldown vamos a ver cuantos alumnos hay por vulnerabilidad
                var queryVulnerablesDetail = (from sv in dbCtx.StudentVulnerabilities
                                             join v in dbCtx.Vulnerabilities on sv.VulnerabilityID equals v.ID
                                             join s in dbCtx.Students on sv.StudentID equals s.ID
                                             join cg in dbCtx.ClassGroups on s.ClassGroupID equals cg.ID
                                             //se contaran solo las del grupo y que tengan mas de 1
                                             where @group == cg.GroupID && sv.VulStatus == 1
                                             group sv by v.Description into g
                                             select new
                                             {
                                                 vulnerability = g.Key,
                                                 quantity = g.Count()
                                             }).ToList();

                //variables para almacenar la cantidad de alumnos por vulnerabilidad
                //se inicia en 0 por que si no hay alumnos entonces no aparecen en el query
                int academica = 0;
                int economica = 0;
                int psicologica = 0;
                int transporte = 0;
                foreach(var vul in queryVulnerablesDetail)
                {
                    //la cantidad se almacena dependiendo la vulnerabilidad
                    if (vul.vulnerability.ToString() == "Académica")
                    {
                        academica = vul.quantity;
                    }
                    if (vul.vulnerability.ToString() == "Económica")
                    {
                        economica = vul.quantity;
                    }
                    if (vul.vulnerability.ToString() == "Psicológica")
                    {
                        psicologica = vul.quantity;
                    }
                    if (vul.vulnerability.ToString() == "Transporte")
                    {
                        transporte = vul.quantity;
                    }
                }
                //guardamos los resultados en viewData para usarlos en la vista
                ViewData["Transporte"] = transporte;
                ViewData["Psicológica"] = psicologica;
                ViewData["Económica"] = economica;
                ViewData["Académica"] = academica;
                #endregion

                //retornamos la vista con el tutor a usar
                return View(objTutor);
            }
            else
            {
                //si no ses inicio sesion no se puede acceder a esta pagina
                return RedirectToAction("Login", "Login");
            }
        }
    }
}
