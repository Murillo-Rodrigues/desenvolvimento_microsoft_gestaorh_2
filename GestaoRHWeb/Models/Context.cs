using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestaoRHWeb.Models
{
    public class Context : IdentityDbContext<Usuario>
    {
        public Context(DbContextOptions options) : base(options) { }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Caixa> Caixas { get; set; }
        public DbSet<Prontuario> Prontuarios { get; set; }
        public DbSet<Solicitacao> Solicitacoes { get; set; }
        public DbSet<ItemSolicitacao> ItensSolicitacao { get; set; }
        public DbSet<UsuarioView> Usuarios { get; set; }

    }
}
