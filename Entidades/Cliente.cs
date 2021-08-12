using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StandByClientes.Entidades
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Razao_Social { get; set; }
        public string Cnpj { get; set; }
        public DateTime? Data_Fundacao { get; set; }
        public double Capital { get; set; }
        public bool Quarentena { get; set; }
        public bool Status_Cliente { get; set; }
        public string Classificacao { get; set; }
    }
}
