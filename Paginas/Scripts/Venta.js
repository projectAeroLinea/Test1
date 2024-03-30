$(document).ready(function () {

    $("#btnActualizar_Venta").click(function () {
        ProcesarComandosVenta("Actualizar");
    });

    $("#btnInsertar_Venta").click(function () {
        ProcesarComandosVenta("Insertar");

    });
    $("#btnEliminar_Venta").click(function () {
        ProcesarComandosVenta("Eliminar");

    });
    $("#btnConsultar_Venta").click(function () {
        ProcesarComandosVenta("Consultar");

    });

    LlenarComboVentaCliente();
    LlenarComboVentaProveedor();
    LlenarComboVentaProducto(true);
    LlenarComboVentaGarantia();
    LlenarTablaVenta();

});

function LlenarComboVentaCliente() {
     LlenarComboControlador("../Comunes/ControladorCombos.ashx", "VENTA_CLIENTE", null, "#cboIdentificacionCliente");


}
function LlenarComboVentaProveedor() {
   LlenarComboControlador("../Comunes/ControladorCombos.ashx", "VENTA_PROVEEDOR", null, "#cboCodigoProveedor");


}
function LlenarComboVentaProducto(Entrada) {

    if (Entrada) {

        LlenarComboControlador("../Comunes/ControladorCombos.ashx", "VENTA_PRODUCTO", null, "#cboCodigoProducto");

    } else {
        $("#cboCodigoProducto").empty();
        let Proveedor = $("#cboCodigoProveedor").val();
        var lstParametros = [{ "Parametro": "@prVentaProveedor", "Valor": Proveedor }];
        var promise = LlenarComboControlador("../Comunes/ControladorCombos.ashx", "VENTA_PRODUCTO_CAMBIO", lstParametros, "#cboCodigoProducto");
       


    }
    
   


}

function LlenarComboVentaGarantia() {
    LlenarComboControlador("../Comunes/ControladorCombos.ashx", "VENTA_GARANTIA", null, "#cboCodigoGarantia");


}
function LlenarTablaVenta() {

    LlenarGridControlador("../Comunes/ControladorGrids.ashx", "TablaVentas", null, "#tblVentas");
}


function ProcesarComandosVenta(Comando) {
    var Codigo_Venta = $("#txtCodigoVenta").val();
    var Identificacion_Cliente = $("#cboIdentificacionCliente").val();
    var Codigo_Garantia = $("#cboCodigoGarantia").val();
    var Codigo_Producto = $("#cboCodigoProducto").val();
    var Codigo_Proveedor = $("#cboCodigoProveedor").val();
    var Cantidad = $("#txtCantidad").val();
    var Fecha_Pedido = $("#txtFechaPedido").val();
    var Fecha_Entrega = $("#txtFechaEntrega").val();



    var DatosVenta = {
        strCodigoVenta: Codigo_Venta,
        strIdentificacionCliente: Identificacion_Cliente,
        strCodigoGarantia: Codigo_Garantia,
        strCodigo_Producto: Codigo_Producto,
        strCodigo_Proveedor: Codigo_Proveedor,
        intCantidad: Cantidad ,
        dateFechaPedido: Fecha_Pedido   ,
        dateFechaEntrega: Fecha_Entrega ,
        strComando: Comando
    }

    if (Comando == "Consultar" || Comando == "Eliminar") {

        DatosVenta.intCantidad = 0
        DatosVenta.dateFechaPedido = "3000-01-01"
        DatosVenta.dateFechaEntrega = "3000-01-01"
    }

    $.ajax({

        type: "POST",
        url: "../Controladores/ControladorVenta.ashx",
        contentType: "json",
        data: JSON.stringify(DatosVenta),
        success: function (RptaVenta) {

            if (Comando != "Consultar") {
                $("#dvMensaje_Venta").addClass("alert alert-success");
                $("#dvMensaje_Venta").html(RptaVenta);
                if (Comando == "Insertar" || Comando == "Actualizar") {

                    ProcesarComandosVenta("Consultar");

                }
                
                LlenarTablaVenta();

            }
            else {
                var Venta = JSON.parse(RptaVenta);
               
                $("#txtCodigoVenta").val(Venta.strCodigoVenta);
                $("#cboIdentificacionCliente").val(Venta.strIdentificacionCliente);
                $("#cboCodigoProveedor").val(Venta.strCodigo_Proveedor);
                LlenarComboVentaProducto(true);
                $("#cboCodigoProducto").val(Venta.strCodigo_Producto);
                $("#cboCodigoGarantia").val(Venta.strCodigoGarantia);
                $("#txtCantidad").val(Venta.intCantidad);
                $("#txtFechaPedido").val(Venta.dateFechaPedido.split('T')[0]);
                $("#txtFechaEntrega").val(Venta.dateFechaEntrega.split('T')[0]);
                $("#txtSubTotal").val(Venta.intSubTotal);
                $("#dvMensaje").html("");
                  
            }
        },
        error: function (RespuestaError) {
            $("#dvMensaje_Venta").addClass("alert alert-danger");
            $("#dvMensaje_Venta").html(RespuestaError);
        }
    });

}

