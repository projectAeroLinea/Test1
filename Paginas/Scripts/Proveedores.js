$(document).ready(function () {

  
    
    $("#btnActualizarProveedor").click(function () {
        ProcesarComandos("Actualizar");
    });
    
    $("#btnInsertarProveedor").click(function () {
        ProcesarComandos("Insertar");
        
    });
    $("#btnEliminarProveedor").click(function () {
        ProcesarComandos("Eliminar");
       
    });
    $("#btnConsultarProveedor").click(function () {
        ProcesarComandos("Consultar");
       
    });

    LlenarComboProveedor();
    LlenarTablaProveedores();
    
    
});

function LlenarTablaProveedores() {
    
    LlenarGridControlador("../Comunes/ControladorGrids.ashx", "TablaProveedor", null, "#tblProveedores");
}

function ProcesarComandos(Comando) {
    var Codigo_Proveedor = $("#txtCodigoProveedor").val();
    var Nombre = $("#txtNombre").val();
    var Direccion = $("#txtDireccion").val();
    var Telefono = $("#txtTelefono").val();
    var Ciudad = $("#txtCiudad").val();
    var Sector = $("#cboSector").val();

    var DatosProveedor = {
        strCodigo_Proveedor: Codigo_Proveedor,
        strNombre: Nombre,
        strDireccion: Direccion,
        strTelefono: Telefono,
        strCiudad: Ciudad,
        strSector: Sector,
        strComando: Comando
    }
    $.ajax({
        
        type: "POST",
        url: "../Controladores/ControladorProveedor.ashx",
        contentType: "json",
        data: JSON.stringify(DatosProveedor),
        success: function (RptaProveedor) {
           
            if (Comando != "Consultar") {
                $("#dvMensaje").addClass("alert alert-success");
                $("#dvMensaje").html(RptaProveedor);
                $("#cboProveedor").empty();
                LlenarComboProducto_Proveedor();
                LlenarTablaProveedores();
                
                $("#txtCodigoProveedor").val("");
                $("#txtNombre").val("");
                $("#txtDireccion").val("");
                $("#txtTelefono").val("");
                $("#txtCiudad").val("");

            }
            else {
                var Proveedor = JSON.parse(RptaProveedor);
                $("#txtNombre").val(Proveedor.strNombre);
                $("#txtDireccion").val(Proveedor.strDireccion);
                $("#txtTelefono").val(Proveedor.strTelefono);
                $("#txtCiudad").val(Proveedor.strCiudad);
                $("#cboSector").val(Proveedor.strSector);
                $("#dvMensaje").html("");
                
              
            }
        },
        error: function (RespuestaError) {
            $("#dvMensaje").addClass("alert alert-danger");
            $("#dvMensaje").html(RespuestaError);
        }
    });

}
function LlenarComboProveedor() {
    var promise = LlenarComboControlador("../Comunes/ControladorCombos.ashx", "PROVEEDOR", null, "#cboSector");
   
    if (promise) {
        promise.then(function (value) {
            //Se invoca el llenado del combo de producto
            
        });
    }
}

