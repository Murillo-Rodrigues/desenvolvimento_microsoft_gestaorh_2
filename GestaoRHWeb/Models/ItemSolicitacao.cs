using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoRHWeb.Models
{
    [Table("ItensSolicitacao")]
    public class ItemSolicitacao : BaseModel
    {
        public ItemSolicitacao()
        {
            Prontuario = new Prontuario();
        }

        public Prontuario Prontuario { get; set; }

        public string Matricula { get; set; }
        public string Custodia { get; set; }
        public string CarrinhoId { get; set; }
    }
}
