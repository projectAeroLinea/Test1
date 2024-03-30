$(document).ready(function () {



    $("#btnActualizarGarantia").click(function () {
        ProcesarComandosGarantia("Actualizar");
    });

    $("#btnInsertarGarantia").click(function () {
        ProcesarComandosGarantia("Insertar");

    });
    $("#btnEliminarGarantia").click(function () {
        ProcesarComandosGarantia("Eliminar");

    });
    $("#btnConsultarGarantia").click(function () {
        ProcesarComandosGarantia("Consultar");

    });

  
    LlenarTablaGarantia();

});

function LlenarTablaGarantia() {

    LlenarGridControlador("../Comunes/ControladorGrids.ashx", "TablaGarantia", null, "#tblGarantia");
}

function ProcesarComandosGarantia(Comando) {
    var Codigo_Garantia = $("#txtCodigoGarantia").val();
    var Descripcion = $("#txtDescripcionGarantia").val();
   

    var DatosGarantia = {
        strCodigo_Garantia: Codigo_Garantia,
        strDescripcion: Descripcion,
        strComando: Comando

       
    }
    $.ajax({

        type: "POST",
        url: "../Controladores/ControladorGarantia.ashx",
        contentType: "json",
        data: JSON.stringify(DatosGarantia),
        success: function (RptaGarantia) {

            if (Comando != "Consultar") {
                $("#dvMensaje_Garantia").addClass("alert alert-success");
                $("#dvMensaje_Garantia").html(RptaGarantia);
                LlenarTablaGarantia();
                $("#txtCodigoGarantia").val("");
                $("#txtDescripcionGarantia").val("");
            }
            else {
                var Garantia = JSON.parse(RptaGarantia);
                $("#txtDescripcionGarantia").val(Garantia.strDescripcion);
                $("#dvMensaje").html("");


            }
        },
        error: function (RespuestaError) {
            $("#dvMensaje").addClass("alert alert-danger");
            $("#dvMensaje").html(RespuestaError);
        }
    });

}
