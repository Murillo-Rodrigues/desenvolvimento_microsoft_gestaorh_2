using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoRHWeb.Models
{
    [Table("Caixas")]
    public class Caixa : BaseModel
    {
        [MinLength(9, ErrorMessage = "Necessário número da Caixa conter 9 dígitos!")]
        [MaxLength(9)]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string NumeroCaixa { get; set; }

        [MinLength(3, ErrorMessage = "Necessário Custódia da Caixa conter 3 dígitos!")]
        [MaxLength(3)]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Custodia { get; set; }

    }
}
