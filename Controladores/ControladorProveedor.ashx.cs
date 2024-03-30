using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using project_Venta_De_Accesorios.Clases;
using project_Venta_De_Accesorios.Modelos;

namespace project_Venta_De_Accesorios.Controladores
{
   
    public class ControladorProveedor : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string DatosProveedor;
                StreamReader reader = new StreamReader(context.Request.InputStream);
                DatosProveedor = reader.ReadToEnd();
                Proveedor Proveedor = JsonConvert.DeserializeObject<Proveedor>(DatosProveedor);

                context.Response.Write(ProcesarComando(Proveedor));
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
        }

        private string ProcesarComando(Proveedor Proveedor)
        {
            if (ValidarDatos(Proveedor))
            {
                clsProveedor proveedor = new clsProveedor();
                proveedor.Proveedor = Proveedor;
                switch (proveedor.Proveedor.strComando.ToUpper())
                {
                    case "INSERTAR":
                        return proveedor.Insertar();
                    case "ACTUALIZAR":
                        return proveedor.Actualizar();
                    case "ELIMINAR":
                        return proveedor.Eliminar();
                    case "CONSULTAR":
                        if (proveedor.Consultar())
                        {
                            return JsonConvert.SerializeObject(proveedor.Proveedor);
                        }
                        else
                        {
                            return proveedor.Proveedor.Error;
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
        private bool ValidarDatos(Proveedor Proveedor)
        {
            if ((String.IsNullOrEmpty(Proveedor.strCodigo_Proveedor) || String.IsNullOrEmpty(Proveedor.strNombre) || String.IsNullOrEmpty(Proveedor.strDireccion) || String.IsNullOrEmpty(Proveedor.strTelefono) || String.IsNullOrEmpty(Proveedor.strCiudad) || Proveedor.strSector== "0") && (Proveedor.strComando == "Insertar" || Proveedor.strComando == "Actualizar"))
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