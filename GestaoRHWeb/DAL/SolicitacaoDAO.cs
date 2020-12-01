using GestaoRHWeb.Models;
using System.Collections.Generic;
using System.Linq;

namespace GestaoRHWeb.DAL
{
    public class SolicitacaoDAO
    {
        private readonly Context _context;

        public SolicitacaoDAO(Context context) => _context = context;


        public void Cadastrar(Solicitacao solicitacao)
        {
            _context.Solicitacoes.Add(solicitacao);
            _context.SaveChanges();


        }
        public List<Solicitacao> Listar() => _context.Solicitacoes.ToList();
    }
}
