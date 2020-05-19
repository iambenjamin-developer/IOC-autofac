function confirmar(id) {

    alertify.confirm("¿Desea dar de baja al Usuario ID " + id.toString() + "?", function (e) {
        if (e) {
            //after clicking OK
            location.href = "/Usuarios/Eliminar/?id=" + id;

        } else {
            //after clicking Cancel          
        }
    });// fin alertify

    //   <button class="btn btn-danger" onclick="location.href='@Url.Action("Eliminar", "Usuarios",  new { id = obj.ID_USUARIO })'"><i class="fas fa-trash-alt"></i></button>
}
