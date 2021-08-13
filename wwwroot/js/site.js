// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



$(document).ready(function () {
    
    $(document).on('click', btnPesquisar, function () { 

        var btnPesquisar = '#btnPesquisar';
        var razaoSocial = '#razao_social';
        var cnpj = '#cnpj';
        var ativo = document.querySelector('input[name=ativo]:checked').value;
        alert(ativo);
       

        //var razaoSocial = $(razao_social).val();
        //var cnpj = $(cnpj).val();
        var razaoSocial = $(razao_social).val();
        var cnpj = $(cnpj).val();
       
        


        alert(razaoSocial);
        alert(cnpj);
      
       

        var pesquisarModel = {
            razaoSocial: razaoSocial,
            cnpj: cnpj,
            ativo: ativo
        }

        $.ajax({
            type: 'POST',
            url: '/clientes/index',
            data: pesquisarModel,
            async: true,
            cache: false,
            success: function (data) {  

                alert(data.cnpj);
                alert(data.razaoSocial);

            },
            error: function () {
                alert('Houve uma falha ao buscar os daos da rota /clientes/pesquisar');
            }
        });
    });

});