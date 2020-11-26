using GestaoRHWeb.DAL;
using GestaoRHWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace GestaoRHWeb.Controllers
{
    public class FuncionarioController : Controller
    {

        private readonly FuncionarioDAO _funcionarioDAO;

        public FuncionarioController(FuncionarioDAO funcionarioDAO) => _funcionarioDAO = funcionarioDAO;

        /* ------------------- INDEX ------------------- */
        public IActionResult Index()
        {

            List<Funcionario> funcionarios = _funcionarioDAO.Listar();
            ViewBag.QuantidadeRegistros = funcionarios.Count();
            return View(funcionarios);
        }

        /* ------------------- CADASTRAR ------------------- */
        public IActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar(Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                if (_funcionarioDAO.Cadastrar(funcionario))
                {
                    TempData["msg"] = "<script>alert('Funcionário cadastrado com sucesso!');</script>";
                    return RedirectToAction("Index", "Funcionario");
                }
                ModelState.AddModelError("", "Não foi possível cadastrar o funcionário! Já existe um funcionário com a mesma matrícula na base de dados");
            }
            return View(funcionario);
        }

        /* ------------------- REMOVER ------------------- */
        public IActionResult Remover(int id)
        {
            Funcionario f = _funcionarioDAO.BuscarPorId(id);
            if (_funcionarioDAO.Remover(f))
            {
                TempData["msg"] = "<script>alert('Funcionário removido com sucesso!');</script>";
            }
            else
            {
                TempData["msg"] = "<script>alert('Não é possivel remover um funcionário com prontuários vinculados!');</script>";
            }

            return RedirectToAction("Index", "Funcionario");
        }

        /* ------------------- ALTERAR ------------------- */
        public IActionResult Alterar(int id)
        {
            return View(_funcionarioDAO.BuscarPorId(id));
        }

        [HttpPost]
        public IActionResult Alterar(Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                _funcionarioDAO.Alterar(funcionario);
                TempData["msg"] = "<script>alert('Alteração realizada com sucesso!');</script>";
                return RedirectToAction("Index", "Funcionario");
            }
            return View(funcionario);
        }

    }
}
