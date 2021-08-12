using System;

namespace StandByClientes.Models
{
    public class ClienteViewModel
    {
        public string ClienteId { get; set; }
        public string Razao_Social { get; set; }
        public string Cnpj { get; set; }
        public DateTime? Data_Fundacao { get; set; }
        public double Capital { get; set; }
        public bool Quarentena
        {
            get
            {
                //Todo cliente com menos de um ano da data de fundação deverá ser inserido na base
                //com a campo quarentena igual a true;
                int tempo = CalculaDiferencaDatasaDatas(DateTime.Now, Convert.ToDateTime(this.Data_Fundacao));

                if (tempo < 365)
                {
                    return true;
                }
                return false;
            }
        }
        public bool Status_Cliente { get; set; }

        public string Classificacao
        {
            get
            {
                //Se o valor o capital da empresa for <= 10.000, 00 sua classificação deverá ser C;
                //• Se o valor o capital da empresa for > 10.000, 00 && <= 1.000.000, 00 sua classificação
                //deverá ser B;
                //• Se o valor o capital da empresa for > 1.000.000, 00 sua classificação deverá ser A.   
                if (this.Capital <= 10.000)
                {
                    return "C";
                }
                if (this.Capital > 10.000)
                {
                    return "C";
                }
                return "A";
            }
        }

        public static int CalculaDiferencaDatasaDatas(DateTime dataFinal, DateTime dataInicial)
        {
            TimeSpan date = Convert.ToDateTime(dataFinal) - Convert.ToDateTime(dataInicial);

            int totalDias = date.Days;
            return totalDias;
        }
    }
}
