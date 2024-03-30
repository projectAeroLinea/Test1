$(document).ready(function () {

    $("#btnActualizar_Cliente").click(function () {
        ProcesarComandosCliente("Actualizar");
    });

    $("#btnInsertar_Cliente").click(function () {
        ProcesarComandosCliente("Insertar");

    });
    $("#btnEliminar_Cliente").click(function () {
        ProcesarComandosCliente("Eliminar");

    });
    $("#btnConsultar_Cliente").click(function () {
        ProcesarComandosCliente("Consultar");

    });

  
    LlenarTablaCliente();


});


function LlenarTablaCliente() {

    LlenarGridControlador("../Comunes/ControladorGrids.ashx", "TablaCliente", null, "#tblClientes");
}

function ProcesarComandosCliente(Comando) {
    var Identificacion_Cliente = $("#txtIdentificacionCliente").val();
    var Nombre_Cliente = $("#txtNombreCliente").val();
    var Direccion_Cliente = $("#txtDireccionCliente").val();
    var Telefono_Cliente = $("#txtTelefonoCliente").val();


    var DatosCliente = {
        strIdentificacion: Identificacion_Cliente,
        strNombre: Nombre_Cliente,
        strDireccion: Direccion_Cliente,
        strTelefono: Telefono_Cliente,
        strComando: Comando
    }



    $.ajax({

        type: "POST",
        url: "../Controladores/ControladorCliente.ashx",
        contentType: "json",
        data: JSON.stringify(DatosCliente),
        success: function (RptaCliente) {

            if (Comando != "Consultar") {
                $("#dvMensaje_Cliente").addClass("alert alert-success");
                $("#dvMensaje_Cliente").html(RptaCliente);
                LlenarTablaCliente();
                $(cboIdentificacionCliente).empty();
                LlenarComboVentaCliente();
                $("#txtIdentificacionCliente").val("");
                $("#txtNombreCliente").val("");
                $("#txtDireccionCliente").val("");
                $("#txtTelefonoCliente").val("");

            }
            else {
                var cliente = JSON.parse(RptaCliente);
                $("#txtIdentificacionCliente").val(cliente.strIdentificacion);
                $("#txtNombreCliente").val(cliente.strNombre);
                $("#txtDireccionCliente").val(cliente.strDireccion);
                $("#txtTelefonoCliente").val(cliente.strTelefono);

                $("#dvMensaje_Cliente").html("");

            }
        },
        error: function (RespuestaError) {
            $("#dvMensaje_Cliente").addClass("alert alert-danger");
            $("#dvMensaje_Cliente").html(RespuestaError);
        }
    });

}
