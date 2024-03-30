using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using project_Venta_De_Accesorios.Modelos;
using project_Venta_De_Accesorios.Clases;

namespace project_Venta_De_Accesorios.Controladores
{
    /// <summary>
    /// Descripción breve de ControladorCliente
    /// </summary>
    public class ControladorCliente : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string DatosCliente;
                StreamReader reader = new StreamReader(context.Request.InputStream);
                DatosCliente = reader.ReadToEnd();
                Cliente Cliente = JsonConvert.DeserializeObject<Cliente>(DatosCliente);

                context.Response.Write(ProcesarComando(Cliente));
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
        }

        private string ProcesarComando(Cliente cliente)
        {
            if (ValidarDatos(cliente))
            {
                clsCliente Cliente = new clsCliente();
                 Cliente.Cliente = cliente;
                switch (Cliente.Cliente.strComando.ToUpper())
                {
                    case "INSERTAR":
                        return Cliente.Insertar();
                    case "ACTUALIZAR":
                        return Cliente.Actualizar();
                    case "ELIMINAR":
                        return Cliente.Eliminar();
                    case "CONSULTAR":
                        if (Cliente.Consultar())
                        {
                            return JsonConvert.SerializeObject(Cliente.Cliente);
                        }
                        else
                        {
                            return Cliente.Cliente.Error;
                        }
                    default:
                        return "NO SE HA DEFINIDO EL COMANDO";
                }
            }
            else
            {

                return "NO INGRESO DATOS COHERENTES A LA INSTRUCCION";
            }
        }

        private bool ValidarDatos(Cliente cliente)
        {
            if ((String.IsNullOrEmpty(cliente.strIdentificacion) || String.IsNullOrEmpty(cliente.strNombre) || String.IsNullOrEmpty(cliente.strDireccion) || String.IsNullOrEmpty(cliente.strTelefono) ) && (cliente.strComando == "Insertar" || cliente.strComando == "Actualizar"))
            {

                return false;

            }


            return true;
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}