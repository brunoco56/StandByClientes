using Microsoft.EntityFrameworkCore;
using StandByClientes.Entidades;
using StandByClientes.Models;

namespace StandByClientes.Contexto
{
    public class Contexto : DbContext
    {
        public DbSet<Cliente> Ciente { get; set; }

        //Construtor que chama o Banco de Dados
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }    

    }
}
