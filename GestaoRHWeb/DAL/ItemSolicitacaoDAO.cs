using GestaoRHWeb.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GestaoRHWeb.DAL
{
    public class ItemSolicitacaoDAO
    {
        private readonly Context _context;

        public ItemSolicitacaoDAO(Context context) => _context = context;

        public ItemSolicitacao BuscarPorId(int id) => _context.ItensSolicitacao.Find(id);
        public ItemSolicitacao BuscarDados(int id) =>
            _context.ItensSolicitacao.
            Include(x => x.Prontuario).
            FirstOrDefault(x => x.Id == id);
        public void Cadastrar(ItemSolicitacao item)
        {
            _context.ItensSolicitacao.Add(item);
            _context.SaveChanges();
        }

        public int Count() => _context.ItensSolicitacao.Count();

        public List<ItemSolicitacao> ListarPorCarrinhoId(string id) =>
            _context.ItensSolicitacao.
            Include(x => x.Prontuario).
            Where(x => x.CarrinhoId == id).ToList();

        public void Remover(int id)
        {
            _context.ItensSolicitacao.Remove(BuscarPorId(id));
            _context.SaveChanges();
        }

        public List<ItemSolicitacao> Listar() =>
            _context.ItensSolicitacao.
            Include(x => x.Prontuario).
            ToList();
    }
}
