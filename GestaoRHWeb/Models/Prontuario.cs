using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoRHWeb.Models
{
    [Table("Prontuarios")]
    public class Prontuario : BaseModel
    {
        public Prontuario()
        {
            Funcionario = new Funcionario();
            Caixa = new Caixa();
            Status = "Disponivel";
        }

        [ForeignKey("FuncionarioId")]
        public Funcionario Funcionario { get; set; }
        public int FuncionarioId { get; set; }

        [ForeignKey("CaixaId")]
        public Caixa Caixa { get; set; }
        public int CaixaId { get; set; }

        public string Status { get; set; }

    }
}
