using GestaoRHWeb.Models;
using System.Collections.Generic;
using System.Linq;

namespace GestaoRHWeb.DAL
{
    public class CaixaDAO
    {
        private readonly Context _context;

        public CaixaDAO(Context context) => _context = context;

        public Caixa BuscarPorId(int id) => _context.Caixas.Find(id);
        public Caixa BuscarPorNumeroCaixa(string numeroCaixa) => _context.Caixas.FirstOrDefault(x => x.NumeroCaixa == numeroCaixa);

        public bool Cadastrar(Caixa caixa)
        {
            if (BuscarPorNumeroCaixa(caixa.NumeroCaixa) == null)
            {
                _context.Add(caixa);
                _context.SaveChanges();

                return true;
            }
            return false;
        }

        public void Remover(int id)
        {
            _context.Caixas.Remove(BuscarPorId(id));
            _context.SaveChanges();
        }

        public void Alterar(Caixa caixa)
        {
            _context.Caixas.Update(caixa);
            _context.SaveChanges();
        }

        public List<Caixa> Listar() => _context.Caixas.ToList();
    }
}
