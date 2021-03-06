﻿using GestaoRHWeb.DAL;
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
                    TempData["msg"] = "<script>alert('Caixa cadastrada com sucesso!');</script>";
                    return RedirectToAction("Index", "Caixa");
                }
                ModelState.AddModelError("", "Não foi possível cadastrar a caixa!");
            }
            return View(caixa);
        }

        /* ------------------- REMOVER ------------------- */
        public IActionResult Remover(int id)
        {
            Caixa c = _caixaDAO.BuscarPorId(id);
            if (_caixaDAO.Remover(c))
            {
                TempData["msg"] = "<script>alert('Caixa removida com sucesso!');</script>";
            }
            else
            {
                TempData["msg"] = "<script>alert('Não é possivel remover uma caixa com prontuários vinculados!');</script>";
            }

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
            if (ModelState.IsValid)
            {
                _caixaDAO.Alterar(caixa);
                TempData["msg"] = "<script>alert('Alteração realizada com sucesso!');</script>";
                return RedirectToAction("Index", "Funcionario");
            }
            return View(caixa);
        }


    }
}
