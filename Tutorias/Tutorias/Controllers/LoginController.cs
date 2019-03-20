using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tutorias.Models;
using Tutorias.ViewModels;

namespace Tutorias.Controllers
{
    public class LoginController : Controller
    {
        public static TutoriasDbContext dbCtx = new TutoriasDbContext();

        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel objLogin)
        {
            if (ModelState.IsValid)
            {
                //si fueramos a usar la encriptacion
                //string encryptedPass = EncryptionDecryption.EncriptarSHA1(objLogin.Password);
                
                //guardamos lo que nos pasó el usuario en variables
                string UserName = objLogin.UserName.ToString();
                string UserPassword = objLogin.UserPassword.ToString();

                //obtenemos el id del tutor con ese usiario y contraseña
                var isLogged = (from t in dbCtx.Tutors
                                where t.UserName.Equals(UserName) && t.UserPassword.Equals(UserPassword)
                                select new
                                {
                                    TutorID = t.ID
                                }).SingleOrDefault();

                //si si se encontró un tutor con los parametros obtenidos entonces
                if (isLogged != null)
                {
                    //LOG
                    //Buscar la carpeta en el proyecto
                    var path = Server.MapPath("~") + @"Files";
                    //nombre del archivo
                    //como aparecia FilesLog.txt se le agregó la diagonal
                    var fileName = "/Log.txt";

                    StreamWriter sw = new StreamWriter(path + fileName, true);
                    //Permite escribir en el archivo .txt
                    sw.WriteLine("Metodo: Login -" + DateTime.Now + "- Entró el tutor con Usuario : " + UserName);
                    //cierra la conexion
                    sw.Close();

                    //se hace la consulta del Id del grupo al que le corresponda el Id del tutor obtenido
                    var group = (from cg in dbCtx.ClassGroups
                                 where cg.TutorID == isLogged.TutorID
                                 select new
                                 {
                                     GroupID = cg.GroupID
                                 }).SingleOrDefault();

                    //guardamos en una variable session el GroupID
                    Session["UserGroup"] = group.GroupID.ToString();
                    //manda a la pagina de index
                    return RedirectToAction("MainPage", "MainPage");
                }
            }
            //si no se inicio sesion correctamente entonces segirá en la pagina de login 
            return View(objLogin);
        }

    }
}
