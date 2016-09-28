using System.Collections.Generic;
using System.Web.Mvc;
using EjemplosCulqi.Models;
using System.Web.Script.Serialization;
using RestSharp;

namespace EjemplosCulqi.Controllers
{
    public class VentaController : Controller
    {
        private string codigo_comercio  = "6Xb9O1C2yFhd";
        private string llave_secreta    = "sM73gPkG4QCfaerOyBIlYnY4tGVT4qP4IuAFpUfct9k=";

        // GET: Venta
        public ActionResult NuevaVenta()
        {
            ViewBag.codigo_comercio = codigo_comercio;
            return View();
        }

        [HttpPost]
        public ActionResult NuevaVenta(VentaData ventaData)
        {
            var venta = new Dictionary<string, string>
            {
                { "token", ventaData.Token },
                { "moneda", ventaData.Moneda },
                { "monto", ventaData.Monto },
                { "descripcion", ventaData.Descripcion },
                { "pedido", ventaData.Pedido },
                { "codigo_pais", ventaData.Codigo_pais },
                { "ciudad", ventaData.Ciudad },
                { "usuario", ventaData.Usuario },
                { "direccion", ventaData.Direccion },
                { "telefono", ventaData.Telefono },
                { "nombres", ventaData.Nombres },
                { "apellidos", ventaData.Apellidos },
                { "correo_electronico", ventaData.Correo_electronico }
            };

            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonVenta = js.Serialize(venta);

            System.Diagnostics.Debug.WriteLine(jsonVenta);

            var client = new RestClient("https://integ-pago.culqi.com/api/v1/cargos");
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