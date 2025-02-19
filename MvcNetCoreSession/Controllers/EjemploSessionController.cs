using Microsoft.AspNetCore.Mvc;
using MvcNetCoreSession.Extensions;
using MvcNetCoreSession.Helpers;
using MvcNetCoreSession.Models;

namespace MvcNetCoreSession.Controllers
{
    public class EjemploSessionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SessionMascotaObject(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    Mascota mascota = new Mascota
                    {
                        Nombre = "Bobby",
                        Raza = "Labrador",
                        Edad = 5
                    };
                    HttpContext.Session.SetObject("MASCOTAOBJECT", mascota);
                    ViewData["MENSAJE"] = "Mascota como Object almacenada";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    Mascota mascota =
                        HttpContext.Session.GetObject<Mascota>("MASCOTAOBJECT");
                    ViewData["MASCOTA"] = mascota;
                }
            }
            return View();
        }

        public IActionResult SessionMascotaJson(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    Mascota mascota = new Mascota 
                    { 
                        Nombre = "Bobby", Raza = "Labrador", Edad = 5 
                    };

                    string jsonMascota =
                        HelperJsonSession.SerializeObject<Mascota>(mascota);
                    HttpContext.Session.SetString("MASCOTA", jsonMascota);
                    ViewData["MENSAJE"] = "Mascota JSON almacenada en session";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    string json =
                        HttpContext.Session.GetString("MASCOTA");
                    Mascota mascota =
                        HelperJsonSession.DeserializeObject<Mascota>(json);
                    ViewData["MASCOTA"] = mascota;
                }
            }
            return View();
        }

        public IActionResult SessionCollection(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    List<Mascota> mascotas = new List<Mascota>
                    {
                        new Mascota { Nombre = "Bobby", Raza = "Labrador", Edad = 5 },
                        new Mascota { Nombre = "Luna", Raza = "Beagle", Edad = 3 },
                        new Mascota { Nombre = "Max", Raza = "Golden Retriever", Edad = 7 },
                        new Mascota { Nombre = "Nina", Raza = "Pomerania", Edad = 2 },
                        new Mascota { Nombre = "Rocky", Raza = "Bulldog", Edad = 4 },
                        new Mascota { Nombre = "Bella", Raza = "Dálmata", Edad = 6 },
                        new Mascota { Nombre = "Simba", Raza = "Pastor Alemán", Edad = 8 }
                    };
                    byte[] data =
                        HelperBinarySession.ObjectToByte(mascotas);
                    HttpContext.Session.Set("MASCOTAS", data);
                    ViewData["MENSAJE"] = "Mascotas almacenadas en session";
                    return View();
                }
                else if (accion.ToLower() == "mostrar")
                {
                    byte[] data = HttpContext.Session.Get("MASCOTAS");
                    List<Mascota> mascotas = (List<Mascota>)
                        HelperBinarySession.ByteToObject(data);
                    return View(mascotas);
                }
            }
            return View();
        }

        public IActionResult SessionSimple(string accion)
        {
            if(accion != null)
            {
                if(accion.ToLower() == "almacenar")
                {            
                    //GUARDAMOS DATOS EN SESSION
                    HttpContext.Session.SetString("nombre", "Programeitor");
                    HttpContext.Session.SetString("hora", 
                        DateTime.Now.ToLongTimeString());
                    ViewData["MENSAJE"] = "Datos almacenados en session";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    //RECUPERAMOS LOS DATOS DE SESSION
                    ViewData["USUARIO"] =
                        HttpContext.Session.GetString("nombre");
                    ViewData["HORA"] =
                        HttpContext.Session.GetString("hora");
                }
            }
            return View();
        }

        public IActionResult SessionMascota(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    Mascota mascota = new Mascota();
                    mascota.Nombre = "Flounder";
                    mascota.Raza = "Pez";
                    mascota.Edad = 5;
                    //PARA ALMACENAR OBJETOS Mascota DEBEMOS
                    //CONVERTIRLOS A byte[]
                    byte[] data = 
                        HelperBinarySession.ObjectToByte(mascota);
                    //ALMACENAMOS EL OBJETO EN SESSION MEDIANTE Set
                    HttpContext.Session.Set("MASCOTA", data);
                    ViewData["MENSAJE"] = "Mascota almacenada en session";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    //RECUPERAMOS LOS BYTES DE MASCOTA DE SESSION
                    byte[] data = HttpContext.Session.Get("MASCOTA");
                    //CONVERTIMOS LOS BYTES A OBJETO MASCOTA
                    Mascota mascota = (Mascota)
                        HelperBinarySession.ByteToObject(data);
                    ViewData["MASCOTA"] = mascota;
                }
            }
            return View();
        }
    }
}
