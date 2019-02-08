function CrearTreeView(data, handlerAllSeleccionarNodo) {
    $('#demoTree').jstree({
        json_data: {
            data: data
        },
        checkbox: {
            real_checkboxes: true,
            real_checkboxes_names: function (n) {
                return [("check_" + (n[0].id || Math.ceil(Math.random() * 10000))), n[0].id]
            }
        },
        plugins: ["themes", "json_data", "ui", "checkbox"]
    }).bind("loaded.jstree", function (event, data) {
        $('#demoTree').on("change_state.jstree", handlerAllSeleccionarNodo).jstree('check_node', 'li[selected=selected]');
    });

};
