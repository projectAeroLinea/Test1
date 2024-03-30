$(document).ready(function () {
    
    $("#btnActualizarProducto").click(function () {
        ProcesarComandosProductos("Actualizar");
    });
   
    $("#btnInsertarProducto").click(function () {
        ProcesarComandosProductos("Insertar");

    });
    $("#btnEliminarProducto").click(function () {
        ProcesarComandosProductos("Eliminar");

    });
    $("#btnConsultarProducto").click(function () {
        ProcesarComandosProductos("Consultar");

    });

    LlenarComboProducto_Proveedor();
    LlenarTablaProducto();


});

function LlenarComboProducto_Proveedor() {
    var promise = LlenarComboControlador("../Comunes/ControladorCombos.ashx", "PRODUCTO_PROVEEDOR", null, "#cboProveedor");

  
}

function LlenarTablaProducto() {

    LlenarGridControlador("../Comunes/ControladorGrids.ashx", "TablaProducto", null, "#tblProducto");
}

function ProcesarComandosProductos(Comando) {
    var Codigo_Producto = $("#txtCodigoProducto").val();
    var Codigo_Proveedor = $("#cboProveedor").val();
    var Nombre = $("#txtNombreProducto").val();
    var Descricion = $("#txtDescripcionProducto").val();
    var Valor_Unitario = $("#txtValorUnitario").val();


    var DatosProducto = {
        strCodigo_Producto: Codigo_Producto,
        strCodigo_Proveedor: Codigo_Proveedor,
        strNombre: Nombre,
        strDescripcion: Descricion,
        intValorUnitario: Valor_Unitario,
        strComando: Comando
    }

    if (Comando == "Consultar" || Comando == "Eliminar" || DatosProducto.intValorUnitario == "") {

        DatosProducto.intValorUnitario=0
    }
        
    $.ajax({
        
        type: "POST",
        url: "../Controladores/ControladorProducto.ashx",
        contentType: "json",
        data: JSON.stringify(DatosProducto),
        success: function (RptaProducto) {

            if (Comando != "Consultar") {
                $("#dvMensaje_2").addClass("alert alert-success");
                $("#dvMensaje_2").html(RptaProducto);
                LlenarTablaProducto();
                $("#txtCodigoProducto").val("");
                $("#txtNombreProducto").val("");
                $("#cboProveedor").val("0");
                $("#txtDescripcionProducto").val("");
                $("#txtValorUnitario").val("");

            }
            else {
                var Producto = JSON.parse(RptaProducto);
                
                $("#txtNombreProducto").val(Producto.strNombre);
                $("#txtDescripcionProducto").val(Producto.strDescripcion);
                $("#txtValorUnitario").val(Producto.intValorUnitario);
               
                $("#dvMensaje").html("");

            }
        },
        error: function (RespuestaError) {
            $("#dvMensaje").addClass("alert alert-danger");
            $("#dvMensaje").html(RespuestaError);
        }
    });

}



