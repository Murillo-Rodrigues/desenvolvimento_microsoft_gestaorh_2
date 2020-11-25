using GestaoRHWeb.DAL;
using GestaoRHWeb.Models;
using GestaoRHWeb.Utils;
using Microsoft.AspNetCore.Mvc;

namespace GestaoRHWeb.Controllers
{
    public class SolicitacaoController : Controller
    {

        private readonly SolicitacaoDAO _solicitacaoDAO;
        private readonly ProntuarioDAO _prontuarioDAO;
        private readonly ItemSolicitacaoDAO _itemSolicitacaoDAO;
        private readonly Sessao _sessao;

        public SolicitacaoController(SolicitacaoDAO solicitacaoDAO, ProntuarioDAO prontuarioDAO, ItemSolicitacaoDAO itemSolicitacaoDAO, Sessao sessao)
        {
            _solicitacaoDAO = solicitacaoDAO;
            _prontuarioDAO = prontuarioDAO;
            _itemSolicitacaoDAO = itemSolicitacaoDAO;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            //Teste Branch Support
            //Teste
            //Implementar os números dos itens do carrinho
            //ViewBag.ListaCount = _itemSolicitacaoDAO.Count();

            ViewBag.ListarProntuarios = _prontuarioDAO.Listar();
            return View(_itemSolicitacaoDAO.ListarPorCarrinhoId(_sessao.BuscarCarrinhoId()));
        }

        public IActionResult AdicionarAoCarrinho(int id)
        {
            Prontuario prontuario = _prontuarioDAO.BuscarPorIdFuncionarioECaixa(id);
            ItemSolicitacao item = new ItemSolicitacao
            {
                Prontuario = prontuario,
                Matricula = prontuario.Funcionario.Matricula,
                Custodia = prontuario.Caixa.Custodia,
                CarrinhoId = _sessao.BuscarCarrinhoId()
            };
            _itemSolicitacaoDAO.Cadastrar(item);

            return RedirectToAction("Index", "Solicitacao");
        }
        public IActionResult CarrinhoCompras()
        {
            return View(_itemSolicitacaoDAO.ListarPorCarrinhoId(_sessao.BuscarCarrinhoId()));
        }

        public IActionResult Remover(int id)
        {
            _itemSolicitacaoDAO.Remover(id);
            return RedirectToAction("Index", "Solicitacao");
        }
    }
}
