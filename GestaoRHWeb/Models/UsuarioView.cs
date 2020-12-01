using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoRHWeb.Models
{
    [Table("Usuarios")]
    public class UsuarioView: BaseModel
    {
        [Display (Name = "E-mail")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Senha { get; set; }

        [Display(Name = "Confirmação da senha")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [NotMapped]
        [Compare ("Senha",ErrorMessage = "Campos não coincidem!")]
        public string ConfirmacaoSenha { get; set; }
    }
}
