using Microsoft.EntityFrameworkCore;

namespace GestaoRHWeb.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Caixa> Caixas { get; set; }
        public DbSet<Prontuario> Prontuarios { get; set; }
        public DbSet<Solicitacao> Solicitacoes { get; set; }
        public DbSet<ItemSolicitacao> ItensSolicitacao { get; set; }

    }
}
