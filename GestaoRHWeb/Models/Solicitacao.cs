using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoRHWeb.Models
{
    [Table("Solicitacoes")]
    public class Solicitacao : BaseModel
    {
        public Solicitacao()
        {
            Usuario = Usuario;
            CarrinhoId = CarrinhoId;
        }

        public string Usuario { get; set; }
        public string CarrinhoId { get; set; }
        public List<ItemSolicitacao> Itens { get; set; }
    }
}
