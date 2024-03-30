using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_Venta_De_Accesorios.Modelos
{
    public class Venta
    {
       public String strCodigoVenta { get; set; }
       public String strIdentificacionCliente { get; set; }
       public String strCodigoGarantia { get; set; }
       public String strCodigo_Producto { get; set; }
       public String strCodigo_Proveedor { get; set; }
       public int intCantidad { get; set; }
       public DateTime dateFechaPedido { get; set; }
       public DateTime dateFechaEntrega { get; set; }
       public int intSubTotal { get;  set; }
       public String strComando { get; set; }
       public int intValorUnitarioProducto { get; set; }
       public String Error { get; set; }

       
    }
}