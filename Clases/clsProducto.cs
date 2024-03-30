using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using libComunes.CapaDatos;
using project_Venta_De_Accesorios.Modelos;

namespace project_Venta_De_Accesorios.Clases
{
    public class clsProducto
    {

        public Producto Producto { get; set; }
        public string Insertar()
        {

            string SQL = "PRODUCTO_INSERTAR";

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;
            oConexion.AgregarParametro("@prstrCodigo_Producto", Producto.strCodigo_Producto);
            oConexion.AgregarParametro("@prstrCodigo_Proveedor", Producto.strCodigo_Proveedor);
            oConexion.AgregarParametro("@prstrNombre", Producto.strNombre);
            oConexion.AgregarParametro("@prstrDescripcion", Producto.strDescripcion);
            oConexion.AgregarParametro("@printValorUnitario", Producto.intValorUnitario);
           

            if (oConexion.EjecutarSentencia())
            {
                return "SE INSERTARON LOS DATOS DEL PRODUCTO EN LA BASE DE DATOS";
            }
            else
            {
                Producto.Error = oConexion.Error;
                return Producto.Error;
            }
        }
        public string Actualizar()
        {
           
            string SQL = "PRODUCTO_ACTUALIZAR"; 

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;
            oConexion.AgregarParametro("@prstrCodigo_Producto", Producto.strCodigo_Producto);
            oConexion.AgregarParametro("@prstrNombre", Producto.strNombre);
            oConexion.AgregarParametro("@prstrDescripcion", Producto.strDescripcion);
            oConexion.AgregarParametro("@printValorUnitario", Producto.intValorUnitario);

            if (oConexion.EjecutarSentencia())
            {
                return "SE ACTUALIZARON LOS DATOS DEL PRODUCTO EN LA BASE DE DATOS";
            }
            else
            {
                Producto.Error = oConexion.Error;
                return Producto.Error;
            }
        }
        public string Eliminar()
        {

            string SQL = "PRODUCTO_ELIMINAR";

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;
            oConexion.AgregarParametro("@prstrCodigo_Producto", Producto.strCodigo_Producto);
            oConexion.AgregarParametro("@prstrCodigo_Proveedor", Producto.strCodigo_Proveedor);

            if (oConexion.EjecutarSentencia())
            {
                return "SE  ELIMINO EL PRODUCTO DE LA BASE DE DATOS";
            }
            else
            {
                Producto.Error = oConexion.Error;
                return Producto.Error;
            }
        }
        public bool Consultar()
        {
            string SQL = "PRODUCTO_CONSULTAR";

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;
            oConexion.AgregarParametro("@prstrCodigo_Producto", Producto.strCodigo_Producto);
            oConexion.AgregarParametro("@prstrCodigo_Proveedor", Producto.strCodigo_Proveedor);

            if (oConexion.Consultar())
            {
                if (oConexion.Reader.HasRows)
                {

                    oConexion.Reader.Read();


                   
                    Producto.strNombre = oConexion.Reader.GetString(0);
                    Producto.strDescripcion= oConexion.Reader.GetString(1);
                    Producto.intValorUnitario = oConexion.Reader.GetInt32(2);
                  
                   

                    return true;
                }
                else
                {
                    Producto.Error = "NO SE ENCONTRARON DATOS DEL PROVEEDOR: " + Producto.strCodigo_Producto;
                    return false;
                }
            }
            else
            {
                Producto.Error = oConexion.Error;
                return false;
            }
        }
    }
}