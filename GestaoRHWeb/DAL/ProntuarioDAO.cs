using GestaoRHWeb.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GestaoRHWeb.DAL
{
    public class ProntuarioDAO
    {
        private readonly Context _context;

        public ProntuarioDAO(Context context) => _context = context;

        public Prontuario BuscarPorMatriculaECaixaP(string matricula) =>
           _context.Prontuarios.Include(x => x.Funcionario).Include(x => x.Caixa).FirstOrDefault(x => x.Funcionario.Matricula == matricula);

        public Prontuario BuscarPorMatriculaP(string matricula) =>
           _context.Prontuarios.FirstOrDefault(x => x.Funcionario.Matricula == matricula);

        public Prontuario BuscarPorNumeroCaixaP(string numeroCaixa) =>
            _context.Prontuarios.FirstOrDefault(x => x.Caixa.NumeroCaixa == numeroCaixa);

        public Prontuario BuscarPorIdFuncionarioECaixa(int id) =>
            _context.Prontuarios.Include(x => x.Funcionario).Include(x => x.Caixa).FirstOrDefault(x => x.Id == id);

        public ItemSolicitacao BuscarPorProntuarioSolicitado(int id) =>
            _context.ItensSolicitacao.FirstOrDefault(x => x.Prontuario.Id == id);

        public Prontuario BuscarPorId(int id) =>
            _context.Prontuarios.Find(id);


        public bool Cadastrar(Prontuario prontuario)
        {
            if (BuscarPorMatriculaECaixaP(prontuario.Funcionario.Matricula) == null)
            {
                _context.Prontuarios.Add(prontuario);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Remover(Prontuario prontuario)
        {
            if (BuscarPorProntuarioSolicitado(prontuario.Id) == null)
            {
                _context.Prontuarios.Remove(prontuario);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void Alterar(Prontuario prontuario)
        {
            _context.Prontuarios.Update(prontuario);
            _context.SaveChanges();
        }
        public List<Prontuario> Listar() => _context.Prontuarios.Include(x => x.Funcionario).Include(x => x.Caixa).ToList();

    }


}
