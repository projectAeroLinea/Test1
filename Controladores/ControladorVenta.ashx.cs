using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using project_Venta_De_Accesorios.Modelos;
using project_Venta_De_Accesorios.Clases;

namespace project_Venta_De_Accesorios.Controladores
{
    /// <summary>
    /// Descripción breve de ControladorVenta
    /// </summary>
    public class ControladorVenta : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string DatosVenta;
                StreamReader reader = new StreamReader(context.Request.InputStream);
                DatosVenta = reader.ReadToEnd();
                Venta Venta = JsonConvert.DeserializeObject<Venta>(DatosVenta);

                context.Response.Write(ProcesarComando(Venta));
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
        }

        private string ProcesarComando(Venta Venta)
        {
            if (ValidarDatos(Venta))
            {
                    clsVenta clsVenta = new clsVenta();
                clsVenta.Venta = Venta;
                int SubTotal;
                switch (clsVenta.Venta.strComando.ToUpper())
                {
                    case "INSERTAR":
                       SubTotal=  CalcularSubTotal(clsVenta);
                        if (SubTotal != -1)
                        {
                  
                            clsVenta.Venta.intSubTotal = SubTotal;
                            return clsVenta.Insertar();
                        }
                        else
                        {
                            return clsVenta.Venta.Error;
                        }
                  
                    case "ACTUALIZAR":
                        SubTotal = CalcularSubTotal(clsVenta);
                        if (SubTotal != -1)
                        {

                            clsVenta.Venta.intSubTotal = SubTotal;
                            return clsVenta.Actualizar();
                        }
                        else
                        {
                            return clsVenta.Venta.Error;
                        }

                    case "ELIMINAR":
                        return clsVenta.Eliminar();
                    case "CONSULTAR":
                        if (clsVenta.Consultar())
                        {
                            return JsonConvert.SerializeObject(clsVenta.Venta);
                        }
                        else
                        {
                            return clsVenta.Venta.Error;
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

        private bool ValidarDatos(Venta Venta)
        {
            if ((String.IsNullOrEmpty(Venta.strCodigoVenta) || String.IsNullOrEmpty(Venta.strIdentificacionCliente) || String.IsNullOrEmpty(Venta.strCodigoGarantia) || String.IsNullOrEmpty(Venta.strCodigo_Producto) || String.IsNullOrEmpty(Venta.strCodigo_Proveedor) || Venta.intCantidad <=0 || Venta.strIdentificacionCliente=="0" || Venta.strCodigo_Proveedor=="0" || Venta.strCodigo_Producto=="0" || Venta.strCodigoGarantia=="0" ) && (Venta.strComando == "Insertar" || Venta.strComando == "Actualizar"))
            {

                return false;

            }

            if (Venta.dateFechaPedido > Venta.dateFechaEntrega)
            {
                return false;
            }
            

                return true;
        }


        private int CalcularSubTotal(clsVenta clsVenta) {
            try
            {
                if(clsVenta.CalcularSubTotal())
                {
                    clsVenta.Venta.intSubTotal = clsVenta.Venta.intValorUnitarioProducto * clsVenta.Venta.intCantidad;
                    return clsVenta.Venta.intSubTotal;

                }
                else
                {
                    return -1;

                }

            } catch(Exception ex)
            {

                return -1;

            }
        

        
        
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