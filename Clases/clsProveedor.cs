using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using project_Venta_De_Accesorios.Modelos;
using libComunes.CapaDatos;
namespace project_Venta_De_Accesorios.Clases
{
    public class clsProveedor
    {
        public Proveedor Proveedor { get; set; }
        public string Insertar()
        {
          
            string SQL = "PROVEEDOR_INSERTAR"; 

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;
            oConexion.AgregarParametro("@prCodigo_Proveedor", Proveedor.strCodigo_Proveedor);
            oConexion.AgregarParametro("@prNombre", Proveedor.strNombre);
            oConexion.AgregarParametro("@prDireccion", Proveedor.strDireccion);
            oConexion.AgregarParametro("@prTelefono", Proveedor.strTelefono);
            oConexion.AgregarParametro("@prCiudad", Proveedor.strCiudad);
            oConexion.AgregarParametro("@prSector", Proveedor.strSector);

            if (oConexion.EjecutarSentencia())
            {
                return "SE INSERTARON LOS DATOS DEL PROVEEDOR EN LA BASE DE DATOS";
            }
            else
            {
                Proveedor.Error = oConexion.Error;
                return Proveedor.Error;
            }
        }
        public string Actualizar()
        {
            //Invocar el método insertar
            //Método para grabar en la base de datos
            string SQL = "PROVEEDOR_ACTUALIZAR"; //Nombre del procedimiento almacenado

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;//Para indicar que es un procedimiento almacenado
            oConexion.AgregarParametro("@prCodigo_Proveedor", Proveedor.strCodigo_Proveedor);
            oConexion.AgregarParametro("@prNombre", Proveedor.strNombre);
            oConexion.AgregarParametro("@prDireccion", Proveedor.strDireccion);
            oConexion.AgregarParametro("@prTelefono", Proveedor.strTelefono);
            oConexion.AgregarParametro("@prCiudad", Proveedor.strCiudad);
            oConexion.AgregarParametro("@prSector", Proveedor.strSector);

            if (oConexion.EjecutarSentencia())
            {
                return "SE ACTUALIZARON LOS DATOS DEL PROVEEDOR EN LA BASE DE DATOS";
            }
            else
            {
                Proveedor.Error = oConexion.Error;
                return Proveedor.Error;
            }
        }
        public string Eliminar()
        {
            
            string SQL = "PROVEEDOR_ELIMINAR"; 

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;
            oConexion.AgregarParametro("@prCodigo_Proveedor", Proveedor.strCodigo_Proveedor);

            if (oConexion.EjecutarSentencia())
            {
                return "SE  ELIMINO EL PROVEEDOR DE LA BASE DE DATOS";
            }
            else
            {
                Proveedor.Error = oConexion.Error;
                return Proveedor.Error;
            }
        }
        public bool Consultar()
        {
            string SQL = "PROVEEDOR_CONSULTAR";

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;
            oConexion.AgregarParametro("@prCodigo_Proveedor", Proveedor.strCodigo_Proveedor);

            if (oConexion.Consultar())
            {
                if (oConexion.Reader.HasRows)
                {
                    
                    oConexion.Reader.Read();

                    
                    Proveedor.strNombre = oConexion.Reader.GetString(0);
                    Proveedor.strDireccion = oConexion.Reader.GetString(1);
                    Proveedor.strTelefono = oConexion.Reader.GetString(2);
                    Proveedor.strCiudad = oConexion.Reader.GetString(3);
                    Proveedor.strSector = oConexion.Reader.GetString(4);
                    return true;
                }
                else
                {
                    Proveedor.Error = "NO SE ENCONTRARON DATOS DEL PROVEEDOR: " + Proveedor.strCodigo_Proveedor;
                    return false;
                }
            }
            else
            {
                Proveedor.Error = oConexion.Error;
                return false;
            }
        }


    }
}