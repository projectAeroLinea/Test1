using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_Venta_De_Accesorios.Modelos
{
    public class Producto
    {
        public String strCodigo_Producto { get; set; }
        public String strCodigo_Proveedor { get; set; }
        public String strNombre { get; set; }
        public String strDescripcion { get; set; }
        public Int32 intValorUnitario { get; set; }
        public String strComando { get; set; }
        public String Error { get; set; }


    }
}