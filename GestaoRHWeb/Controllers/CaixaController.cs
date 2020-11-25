using GestaoRHWeb.DAL;
using GestaoRHWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace GestaoRHWeb.Controllers
{
    public class CaixaController : Controller
    {
        private readonly CaixaDAO _caixaDAO;

        public CaixaController(CaixaDAO caixaDAO) => _caixaDAO = caixaDAO;


        /* ------------------- INDEX ------------------- */
        public IActionResult Index()
        {
            List<Caixa> caixas = _caixaDAO.Listar();
            ViewBag.QuantidadeRegistros = caixas.Count();
            return View(caixas);
        }


        /* ------------------- CADASTRAR ------------------- */
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Caixa caixa)
        {
            if (ModelState.IsValid)
            {
                if (_caixaDAO.Cadastrar(caixa))
                {
                    return RedirectToAction("Index", "Caixa");
                }
                ModelState.AddModelError("", "Não foi possível cadastrar a caixa!");
            }
            return View(caixa);
        }

        /* ------------------- REMOVER ------------------- */
        public IActionResult Remover(int id)
        {
            _caixaDAO.Remover(id);
            return RedirectToAction("Index", "Caixa");
        }


        /* ------------------- ALTERAR ------------------- */
        public IActionResult Alterar(int id)
        {

            return View(_caixaDAO.BuscarPorId(id));
        }


        [HttpPost]

        public IActionResult Alterar(Caixa caixa)
        {
            _caixaDAO.Alterar(caixa);
            return RedirectToAction("Index", "Caixa");
        }


    }
}
