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
                string path = queryFoto.CURP.ToString() + ".JPG";

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
