using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoRHWeb.Models
{
    [Table("Solicitacoes")]
    public class Solicitacao : BaseModel
    {
        public Solicitacao()
        {
            Itens = new List<ItemSolicitacao>();
        }

        public Funcionario Funcionario { get; set; }
        public Caixa Caixa { get; set; }
        public List<ItemSolicitacao> Itens { get; set; }
    }
}
