$(document).ready(function () {
  
    $(document).on('click', Pesquisar, function () {

        var Pesquisar = $('#Pesquisar').val();
        var ativo = document.querySelector('input[name=Ativo]:checked').value
        var razaoSocial = $('#razaosocial').val();
        var cnpj = $('#cnpj').val();

		var pesquisarModel = {
			razaoSocial: razaoSocial,
            cnpj: cnpj,
            Ativo: ativo
        }

        $.ajax({
            data: pesquisarModel,
            type: 'POST',
            url: '/Clientes/Index',            
            async: true,
            cache: false,
            success: function (data) {
               // $("#divClientes").html(data);               
            },
            error: function () {
                alert('Houve uma falha ao buscar os daos da rota /clientes/pesquisar');
            }
        });
	});

});