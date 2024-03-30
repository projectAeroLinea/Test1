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
    
    public class ControladorGarantia : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string DatosGarantia;
                StreamReader reader = new StreamReader(context.Request.InputStream);
                DatosGarantia = reader.ReadToEnd();
                Garantia Garantia = JsonConvert.DeserializeObject<Garantia>(DatosGarantia);

                context.Response.Write(ProcesarComando(Garantia));
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
        }
        private string ProcesarComando(Garantia Garantia)
        {
            if (ValidarDatos(Garantia))
            {
                    clsGarantia clsGarantia = new clsGarantia();
                clsGarantia.Garantia = Garantia;
                switch (clsGarantia.Garantia.strComando.ToUpper())
                {
                    case "INSERTAR":
                        return clsGarantia.Insertar();
                    case "ACTUALIZAR":
                        return clsGarantia.Actualizar();
                    case "ELIMINAR":
                        return clsGarantia.Eliminar();
                    case "CONSULTAR":
                        if (clsGarantia.Consultar())
                        {
                            return JsonConvert.SerializeObject(clsGarantia.Garantia);
                        }
                        else
                        {
                            return clsGarantia.Garantia.Error;
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

        private bool ValidarDatos(Garantia Garantia)
        {
            if ((String.IsNullOrEmpty(Garantia.strCodigo_Garantia) || String.IsNullOrEmpty(Garantia.strDescripcion) ) && (Garantia.strComando == "Insertar" || Garantia.strComando == "Actualizar"))
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