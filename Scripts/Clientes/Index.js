var btnPesquisar = '#btnPesquisar';
var txtRazaoSocial = '#razao-social';
var txtCnpj = '#cnpj';

$(document).ready(function () {
	alert('aqui');
	$(document).on('click', btnPesquisar, function () {
		var razaoSocial = $(txtRazaoSocial).val();
		var cnpj = $(txtCnpj).val();

		var pesquisarModel = {
			razaoSocial: razaoSocial,
			cnpj : cnpj
		}

        $.ajax({
            type: 'POST',
            url: '/clientes/pesquisar',
            data: pesquisarModel,
            async: true,
            cache: false,
            success: function (data) {

            },
            error: function () {
                alert('HOuve uma falha ao buscar os daos da rota /clientes/pesquisar');
            }
        });
	});

});