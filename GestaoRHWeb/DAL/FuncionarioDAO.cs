using GestaoRHWeb.Models;
using System.Collections.Generic;
using System.Linq;

namespace GestaoRHWeb.DAL
{
    public class FuncionarioDAO
    {
        private readonly Context _context;

        public FuncionarioDAO(Context context) => _context = context;
        public Funcionario BuscarPorId(int id) => _context.Funcionarios.Find(id);
        public Funcionario BuscarPorMatricula(string matricula) => _context.Funcionarios.FirstOrDefault(x => x.Matricula == matricula);

        public Prontuario BuscarPorIdFuncionarioNoProntuario(int id) => _context.Prontuarios.FirstOrDefault(x => x.Funcionario.Id == id);

        public bool Cadastrar(Funcionario funcionario)
        {
            if (BuscarPorMatricula(funcionario.Matricula) == null)
            {
                _context.Funcionarios.Add(funcionario);
                _context.SaveChanges();

                return true;
            }
            return false;
        }

        public bool Remover(Funcionario f)
        {
            if (BuscarPorIdFuncionarioNoProntuario(f.Id) == null)
            {
                _context.Funcionarios.Remove(f);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void Alterar(Funcionario funcionario)
        {
            _context.Funcionarios.Update(funcionario);
            _context.SaveChanges();
        }

        public List<Funcionario> Listar() => _context.Funcionarios.ToList();

    }
}
