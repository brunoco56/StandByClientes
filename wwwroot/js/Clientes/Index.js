var btnPesquisar = '#btnPesquisar';
var txtRazaoSocial = '#razao-social';
var txtCnpj = '#cnpj';

$(document).ready(function () {
	
    $(document).on('click', Pesquisar, function () {
        alert();
		var razaoSocial = $(txtRazaoSocial).val();
		var cnpj = $(txtCnpj).val();

		var pesquisarModel = {
			razaoSocial: razaoSocial,
			cnpj : cnpj
		}

        $.ajax({
            type: 'POST',
            url: '/Clientes/Index',
            data: pesquisarModel,
            async: true,
            cache: false,
            success: function (data) {

            },
            error: function () {
                alert('Houve uma falha ao buscar os daos da rota /clientes/pesquisar');
            }
        });
	});

});