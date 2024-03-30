using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using libComunes.CapaDatos;
using project_Venta_De_Accesorios.Modelos;
namespace project_Venta_De_Accesorios.Clases
{
    public class clsVenta
    {
        public Venta Venta { get; set; }

        public Boolean CalcularSubTotal()
        {
            string SQL = "CALCULAR_SUBTOTAL";

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;
            oConexion.AgregarParametro("@prstrCodigo_Producto", Venta.strCodigo_Producto);
            if (oConexion.Consultar())
            {
                if (oConexion.Reader.HasRows)
                {

                    oConexion.Reader.Read();

                    Venta.intValorUnitarioProducto = oConexion.Reader.GetInt32(0);

                    return true;



                }
                else
                {
                    Venta.Error = "NO SE ENCONTRARON DATOS DE LA VENTA: " + Venta.strCodigoVenta;
                    return false; ;
                }
            }
            else
            {
                Venta.Error = oConexion.Error;
                return false; ;
            }

        }
        public string Insertar()
        {

            string SQL = "VENTA_INSERTAR";

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;
            oConexion.AgregarParametro("@prstrCodigoVenta", Venta.strCodigoVenta);
            oConexion.AgregarParametro("@prstrIdentificacionCliente", Venta.strIdentificacionCliente);
            oConexion.AgregarParametro("@prstrCodigoGarantia", Venta.strCodigoGarantia);
            oConexion.AgregarParametro("@prstrCodigo_Producto", Venta.strCodigo_Producto);
            oConexion.AgregarParametro("@prstrCodigo_Proveedor", Venta.strCodigo_Proveedor);
            oConexion.AgregarParametro("@printCantidad", Venta.intCantidad);
            oConexion.AgregarParametro("@prdateFechaPedido", Venta.dateFechaPedido);
            oConexion.AgregarParametro("@prdateFechaEntrega", Venta.dateFechaEntrega);
            oConexion.AgregarParametro("@prSubTotal", Venta.intSubTotal);

            if (oConexion.EjecutarSentencia())
            {
                return "SE INSERTARON LOS DATOS DE LA VENTA EN LA BASE DE DATOS";
            }
            else
            {
                Venta.Error = oConexion.Error;
                return Venta.Error;
            }
        }
        public string Actualizar()
        {
           
            string SQL = "VENTA_ACTUALIZAR"; 

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;
            oConexion.AgregarParametro("@prstrCodigoVenta", Venta.strCodigoVenta);
            oConexion.AgregarParametro("@prstrIdentificacionCliente", Venta.strIdentificacionCliente);
            oConexion.AgregarParametro("@prstrCodigoGarantia", Venta.strCodigoGarantia);
            oConexion.AgregarParametro("@prstrCodigo_Producto", Venta.strCodigo_Producto);
            oConexion.AgregarParametro("@prstrCodigo_Proveedor", Venta.strCodigo_Proveedor);
            oConexion.AgregarParametro("@printCantidad", Venta.intCantidad);
            oConexion.AgregarParametro("@prdateFechaPedido", Venta.dateFechaPedido);
            oConexion.AgregarParametro("@prdateFechaEntrega", Venta.dateFechaEntrega);
            oConexion.AgregarParametro("@prSubTotal", Venta.intSubTotal);

            if (oConexion.EjecutarSentencia())
            {
                return "SE ACTUALIZARON LOS DATOS DE LA VENTA EN LA BASE DE DATOS";
            }
            else
            {
                Venta.Error = oConexion.Error;
                return Venta.Error;
            }
        }
        public string Eliminar()
        {

            string SQL = "VENTA_ELIMINAR";

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;
            oConexion.AgregarParametro("@prstrCodigoVenta", Venta.strCodigoVenta);

            if (oConexion.EjecutarSentencia())
            {
                return "SE  ELIMINO LA VENTA DE LA BASE DE DATOS";
            }
            else
            {
                Venta.Error = oConexion.Error;
                return Venta.Error;
            }
        }
        public bool Consultar()
        {
            string SQL = "VENTA_CONSULTAR";

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;
            oConexion.AgregarParametro("@prstrCodigoVenta", Venta.strCodigoVenta);

            if (oConexion.Consultar())
            {
                if (oConexion.Reader.HasRows)
                {

                    oConexion.Reader.Read();

                    Venta.strIdentificacionCliente = oConexion.Reader.GetString(0);
                    Venta.strCodigoGarantia = oConexion.Reader.GetString(1);
                    Venta.strCodigo_Producto = oConexion.Reader.GetString(2);
                    Venta.strCodigo_Proveedor = oConexion.Reader.GetString(3);
                    Venta.intCantidad = oConexion.Reader.GetInt32(4);
                    Venta.dateFechaPedido = oConexion.Reader.GetDateTime(5);
                    Venta.dateFechaEntrega = oConexion.Reader.GetDateTime(6);
                    Venta.intSubTotal = oConexion.Reader.GetInt32(7);
                    return true;
                }
                else
                {
                    Venta.Error = "NO SE ENCONTRARON DATOS DE LA VENTA: " + Venta.strCodigoVenta;
                    return false;
                }
            }
            else
            {
                Venta.Error = oConexion.Error;
                return false;
            }
        }


    }
}