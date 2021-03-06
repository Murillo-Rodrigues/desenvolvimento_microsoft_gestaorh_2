﻿using GestaoRHWeb.DAL;
using GestaoRHWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace GestaoRHWeb.Controllers
{
    public class ProntuarioController : Controller
    {
        private readonly FuncionarioDAO _funcionarioDAO;

        private readonly ProntuarioDAO _prontuarioDAO;

        private readonly CaixaDAO _caixaDAO;

        public ProntuarioController(ProntuarioDAO prontuarioDAO, FuncionarioDAO funcionarioDAO, CaixaDAO caixaDAO)
        {
            _prontuarioDAO = prontuarioDAO;
            _funcionarioDAO = funcionarioDAO;
            _caixaDAO = caixaDAO;
        }


        /* ------------------- INDEX ------------------- */
        public IActionResult Index()
        {
            List<Prontuario> prontuarios = _prontuarioDAO.Listar();
            ViewBag.QuantidadeRegistros = prontuarios.Count();
            return View(prontuarios);
        }


        /* ------------------- CADASTRAR ------------------- */
        public IActionResult Cadastrar()
        {

            ViewBag.listaMatriculas = new SelectList(_funcionarioDAO.Listar(), "Id", "Matricula");
            ViewBag.listaCustodias = new SelectList(_caixaDAO.Listar(), "Id", "Custodia");

            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Prontuario prontuario)
        {
            if (prontuario.FuncionarioId != 0 && prontuario.CaixaId != 0)
            {
                //if (ModelState.IsValid)
                //{
                prontuario.Funcionario = _funcionarioDAO.BuscarPorId(prontuario.FuncionarioId);
                prontuario.Caixa = _caixaDAO.BuscarPorId(prontuario.CaixaId);
                if (_prontuarioDAO.Cadastrar(prontuario))
                {
                    TempData["msg"] = "<script>alert('Prontuário cadastrado com sucesso!');</script>";
                    return RedirectToAction("Index", "Prontuario");
                }
                ModelState.AddModelError("", "Não foi possível realizar o cadastro! Dados já vinculados em outro prontuário!");
                //}
            }
            else
            {
                ModelState.AddModelError("", "Não é possível cadastrar um prontuário com um ou mais dados nulos! Por favor, selecione uma matrícula e uma custódia!");
            }
            ViewBag.listaMatriculas = new SelectList(_funcionarioDAO.Listar(), "Id", "Matricula");
            ViewBag.listaCustodias = new SelectList(_caixaDAO.Listar(), "Id", "Custodia");

            return View(prontuario);
        }

        /* ------------------- REMOVER ------------------- */
        public IActionResult Remover(int id)
        {
            Prontuario p = _prontuarioDAO.BuscarPorId(id);
            if (_prontuarioDAO.Remover(p))
            {
                TempData["msg"] = "<script>alert('Prontuário removido com sucesso!');</script>";
            }
            else
            {
                TempData["msg"] = "<script>alert('Erro! Prontuário está em solicitação e não pode ser excluído da base de dados!');</script>";
            }
            return RedirectToAction("Index", "Prontuario");
        }


        /* ------------------- ALTERAR ------------------- */
        public IActionResult Alterar(int id)
        {
            ViewBag.listaMatriculas = new SelectList(_funcionarioDAO.Listar(), "Id", "Matricula", id);
            ViewBag.listaCustodias = new SelectList(_caixaDAO.Listar(), "Id", "Custodia", id);
            return View(_prontuarioDAO.BuscarPorId(id));
        }


        [HttpPost]

        public IActionResult Alterar(Prontuario prontuario)
        {
            prontuario.Funcionario = _funcionarioDAO.BuscarPorId(prontuario.FuncionarioId);
            prontuario.Caixa = _caixaDAO.BuscarPorId(prontuario.CaixaId);
            _prontuarioDAO.Alterar(prontuario);
            return RedirectToAction("Index", "Prontuario");
        }

    }
}
