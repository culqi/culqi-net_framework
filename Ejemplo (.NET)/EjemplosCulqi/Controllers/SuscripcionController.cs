using System.Collections.Generic;
using System.Web.Mvc;
using EjemplosCulqi.Models;
using System.Web.Script.Serialization;
using RestSharp;

namespace EjemplosCulqi.Controllers
{
    public class SuscripcionController : Controller
    {
        private string codigo_comercio = "v2JdOBEqI1XL";
        private string llave_secreta = "zyIImPG8IcFX4jmwoEHPTVqfbW9h7OcYlEw/9Ul0l68=";
        // GET: Suscripcion
        public ActionResult NuevaSuscripcion()
        {
            ViewBag.codigo_comercio = codigo_comercio;
            return View();
        }

        [HttpPost]
        public ActionResult NuevaSuscripcion(suscripcionData suscripcionData)
        {
            var venta = new Dictionary<string, string>
            {
                { "codigo_comercio", suscripcionData.Codigo_comercio },
                { "codigo_pais", suscripcionData.Codigo_pais },
                { "direccion", suscripcionData.Direccion },
                { "ciudad", suscripcionData.Ciudad },
                { "telefono", suscripcionData.Telefono },
                { "nombre", suscripcionData.Nombre },
                { "apellido", suscripcionData.Apellido },
                { "correo_electronico", suscripcionData.Correo_electronico },
                { "usuarioId", suscripcionData.UsuarioId },
                { "plan_id", suscripcionData.Plan_id },
                { "token", suscripcionData.Token }
            };

            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonVenta = js.Serialize(venta);

            System.Diagnostics.Debug.WriteLine(jsonVenta);

            var client = new RestClient("https://integ-pago.culqi.com/api/v1/suscripciones");
            var request = new RestRequest();
            request.Method = Method.POST;
            request.AddHeader("authorization", "Bearer " + llave_secreta);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", jsonVenta, ParameterType.RequestBody);
            var response = client.Execute(request);
            var content = response.Content;

            System.Diagnostics.Debug.WriteLine(content);
            return View();
        }
    }
}