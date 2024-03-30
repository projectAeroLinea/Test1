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
    /// Descripción breve de ControladorProducto
    /// </summary>
    public class ControladorProducto : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string DatosProducto;
                StreamReader reader = new StreamReader(context.Request.InputStream);
                DatosProducto = reader.ReadToEnd();
                Producto Producto = JsonConvert.DeserializeObject<Producto>(DatosProducto);

                context.Response.Write(ProcesarComando(Producto));
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
        }

        private string ProcesarComando(Producto Producto)
        {
            if (ValidarDatos(Producto))
            {
                clsProducto producto = new clsProducto();
                producto.Producto = Producto;

                switch (producto.Producto.strComando.ToUpper())
                {
                    case "INSERTAR":
                        return producto.Insertar();
                    case "ACTUALIZAR":
                        return producto.Actualizar();
                    case "ELIMINAR":
                        return producto.Eliminar();
                    case "CONSULTAR":
                        if (producto.Consultar())
                        {
                            return JsonConvert.SerializeObject(producto.Producto);
                        }
                        else
                        {
                            return producto.Producto.Error;
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

        private bool ValidarDatos(Producto producto)
        {
            if ((String.IsNullOrEmpty(producto.strCodigo_Producto) || String.IsNullOrEmpty(producto.strCodigo_Proveedor) || String.IsNullOrEmpty(producto.strNombre) || String.IsNullOrEmpty(producto.strDescripcion)  || producto.intValorUnitario <= 0) && (producto.strComando== "Insertar" || producto.strComando == "Actualizar"))
            {
                if ((!String.IsNullOrEmpty(producto.strCodigo_Producto) || !String.IsNullOrEmpty(producto.strCodigo_Proveedor) && (producto.strComando == "Insertar"))) {
                    return true;
                }
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