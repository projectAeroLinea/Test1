using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using libComunes.CapaDatos;
using project_Venta_De_Accesorios.Modelos;

namespace project_Venta_De_Accesorios.Clases
{
    public class clsGarantia
    {
        public Garantia Garantia { get; set; }
        public string Insertar()
        {

            string SQL = "GARANTIA_INSERTAR";

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;
            oConexion.AgregarParametro("@prstrCodigo_Garantia", Garantia.strCodigo_Garantia);
            oConexion.AgregarParametro("@prstrDescripcion", Garantia.strDescripcion);
          

            if (oConexion.EjecutarSentencia())
            {
                return "SE INSERTARON LOS DATOS DE LA GARANTIA EN LA BASE DE DATOS";
            }
            else
            {
                Garantia.Error = oConexion.Error;
                return Garantia.Error;
            }
        }
        public string Actualizar()
        {
            
            string SQL = "GARANTIA_ACTUALIZAR"; 

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;
            oConexion.AgregarParametro("@prstrCodigo_Garantia", Garantia.strCodigo_Garantia);
            oConexion.AgregarParametro("@prstrDescripcion", Garantia.strDescripcion);

            if (oConexion.EjecutarSentencia())
            {
                return "SE ACTUALIZARON LOS DATOS DE LA GARANTIA EN LA BASE DE DATOS";
            }
            else
            {
                Garantia.Error = oConexion.Error;
                return Garantia.Error;
            }
        }
        public string Eliminar()
        {

            string SQL = "GARANTIA_ELIMINAR";

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;
            oConexion.AgregarParametro("@prstrCodigo_Garantia", Garantia.strCodigo_Garantia);

            if (oConexion.EjecutarSentencia())
            {
                return "SE  ELIMINO LA GARANTIA DE LA BASE DE DATOS";
            }
            else
            {
                Garantia.Error = oConexion.Error;
                return Garantia.Error;
            }
        }
        public bool Consultar()
        {
            string SQL = "GARANTIA_CONSULTAR";

            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.StoredProcedure = true;
            oConexion.AgregarParametro("@prstrCodigo_Garantia", Garantia.strCodigo_Garantia);

            if (oConexion.Consultar())
            {
                if (oConexion.Reader.HasRows)
                {

                    oConexion.Reader.Read();


                    Garantia.strDescripcion = oConexion.Reader.GetString(0);
                    
                    return true;
                }
                else
                {
                    Garantia.Error = "NO SE ENCONTRARON DATOS DE LA GARANTIA: " + Garantia.strCodigo_Garantia;
                    return false;
                }
            }
            else
            {
                Garantia.Error = oConexion.Error;
                return false;
            }
        }


    }
}