using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using libComunes.CapaDatos;
using project_Venta_De_Accesorios.Modelos;

namespace project_Venta_De_Accesorios.Clases
{
    public class clsCliente
    {

        public Cliente Cliente { get; set; }
        public string Insertar()
        {

            string SQL = "CLIENTE_INSERTAR";

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;
            oConexion.AgregarParametro("@prstrIdentificacion", Cliente.strIdentificacion);
            oConexion.AgregarParametro("@prstrNombre", Cliente.strNombre);
            oConexion.AgregarParametro("@prstrDireccion", Cliente.strDireccion);
            oConexion.AgregarParametro("@prstrTelefono", Cliente.strTelefono);
        


            if (oConexion.EjecutarSentencia())
            {
                return "SE INSERTARON LOS DATOS DEL CLIENTE EN LA BASE DE DATOS";
            }
            else
            {
                Cliente.Error = oConexion.Error;
                return Cliente.Error;
            }
        }
        public string Actualizar()
        {

            string SQL = "CLIENTE_ACTUALIZAR";

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;
            oConexion.AgregarParametro("@prstrIdentificacion", Cliente.strIdentificacion);
            oConexion.AgregarParametro("@prstrNombre", Cliente.strNombre);
            oConexion.AgregarParametro("@prstrDireccion", Cliente.strDireccion);
            oConexion.AgregarParametro("@prstrTelefono", Cliente.strTelefono);

            if (oConexion.EjecutarSentencia())
            {
                return "SE ACTUALIZARON LOS DATOS DEL CLIENTE EN LA BASE DE DATOS";
            }
            else
            {
                Cliente.Error = oConexion.Error;
                return Cliente.Error;
            }
        }
        public string Eliminar()
        {

            string SQL = "CLIENTE_ELIMINAR";

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;
            oConexion.AgregarParametro("@prstrIdentificacion", Cliente.strIdentificacion);
           

            if (oConexion.EjecutarSentencia())
            {
                return "SE  ELIMINO EL CLIENTE DE LA BASE DE DATOS";
            }
            else
            {
                Cliente.Error = oConexion.Error;
                return Cliente.Error;
            }
        }
        public bool Consultar()
        {
            string SQL = "CLIENTE_CONSULTAR";

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;
            oConexion.AgregarParametro("@prstrIdentificacion", Cliente.strIdentificacion);

            if (oConexion.Consultar())
            {
                if (oConexion.Reader.HasRows)
                {

                    oConexion.Reader.Read();



                    Cliente.strNombre = oConexion.Reader.GetString(0);
                    Cliente.strDireccion = oConexion.Reader.GetString(1);
                    Cliente.strTelefono = oConexion.Reader.GetString(2);



                    return true;
                }
                else
                {
                    Cliente.Error = "NO SE ENCONTRARON DATOS DEL CLIENTE: " + Cliente.strIdentificacion;
                    return false;
                }
            }
            else
            {
                Cliente.Error = oConexion.Error;
                return false;
            }
        }
    }
}