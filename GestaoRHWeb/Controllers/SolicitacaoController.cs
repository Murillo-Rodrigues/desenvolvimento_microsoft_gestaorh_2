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
            _itemSolicitacaoDAO.Remover(id);
            return RedirectToAction("Index", "Solicitacao");
        }

        public IActionResult RegistrarSolicitacao(ItemSolicitacao itens)
        {
            Solicitacao s = new Solicitacao();
            s.Itens.Add(
                new ItemSolicitacao
                {
                    Prontuario = itens.Prontuario
                });

            return RedirectToAction("Index", "Solicitacao");
        }
    }
}
