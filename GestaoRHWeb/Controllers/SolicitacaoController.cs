using GestaoRHWeb.DAL;
using GestaoRHWeb.Models;
using GestaoRHWeb.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GestaoRHWeb.Controllers
{
    [Authorize]
    public class SolicitacaoController : Controller
    {

        private readonly SolicitacaoDAO _solicitacaoDAO;
        private readonly ProntuarioDAO _prontuarioDAO;
        private readonly ItemSolicitacaoDAO _itemSolicitacaoDAO;
        private readonly Sessao _sessao;
        private readonly UserManager<Usuario> _userManager;

        public SolicitacaoController(SolicitacaoDAO solicitacaoDAO, ProntuarioDAO prontuarioDAO, ItemSolicitacaoDAO itemSolicitacaoDAO, Sessao sessao, UserManager<Usuario> userManager)
        {
            _solicitacaoDAO = solicitacaoDAO;
            _prontuarioDAO = prontuarioDAO;
            _itemSolicitacaoDAO = itemSolicitacaoDAO;
            _sessao = sessao;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            //Teste Branch Support
            //Teste
            //Implementar os números dos itens do carrinho
            //ViewBag.Count = _itemSolicitacaoDAO.Count();

            ViewBag.ListarProntuarios = _prontuarioDAO.Listar();
            return View(_itemSolicitacaoDAO.ListarPorCarrinhoId(_sessao.BuscarCarrinhoId()));
        }

        public IActionResult AdicionarAoCarrinho(int id)
        {
            Prontuario prontuario = _prontuarioDAO.BuscarPorIdFuncionarioECaixa(id);

            if (prontuario.Status == "Em Solicitação")
            {
                TempData["msg"] = "<script>alert('Não é possível solicitar um Prontuário uma vez que este já se encontra em estado de solicitação!');</script>";
            }
            else
            {
                prontuario.Status = "Em Solicitação";
                ItemSolicitacao item = new ItemSolicitacao
                {
                    Prontuario = prontuario,
                    Matricula = prontuario.Funcionario.Matricula,
                    Custodia = prontuario.Caixa.Custodia,
                    CarrinhoId = _sessao.BuscarCarrinhoId()
                };
                _itemSolicitacaoDAO.Cadastrar(item);
            }
            return RedirectToAction("Index", "Solicitacao");
        }

        public IActionResult Status()
        {
            //return View(_itemSolicitacaoDAO.ListarPorCarrinhoId(_sessao.BuscarCarrinhoId()));
            return View(ViewBag.Listar = _itemSolicitacaoDAO.Listar());
        }

        public IActionResult Remover(int id)
        {
            ItemSolicitacao itemSolicitacao = _itemSolicitacaoDAO.BuscarDados(id);
            itemSolicitacao.Prontuario.Status = "Disponivel";
            Prontuario procurarProntuarioComDados = _prontuarioDAO.BuscarPorIdFuncionarioECaixa(itemSolicitacao.Prontuario.Id);
            _prontuarioDAO.Alterar(procurarProntuarioComDados);
            return RedirectToAction("Index", "Solicitacao");
        }

        public IActionResult RegistrarSolicitacao()
        {

            Solicitacao solicitacao = new Solicitacao
            {
                Usuario = User.Identity.Name,
                CarrinhoId = _sessao.BuscarCarrinhoId()
            };

            _solicitacaoDAO.Cadastrar(solicitacao);

            TempData["msg"] = "<script>alert('Solicitação realizada!');</script>";
            return RedirectToAction("Index", "Solicitacao");
        }
        public IActionResult ListarCadastradas()
        {
            List<Solicitacao> solicitacoes = _solicitacaoDAO.Listar();
            return View(solicitacoes);
        }


    }
}
