using GestaoRHWeb.DAL;
using GestaoRHWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestaoRHWeb.Controllers
{
    public class StatusController : Controller
    {
        private readonly ItemSolicitacaoDAO _itemSolicitacaoDAO;
        private readonly ProntuarioDAO _prontuarioDAO;


        public StatusController(ItemSolicitacaoDAO itemSolicitacaoDAO, ProntuarioDAO prontuarioDAO)
        {
            _itemSolicitacaoDAO = itemSolicitacaoDAO;
            _prontuarioDAO = prontuarioDAO;
        }


        public IActionResult Remover(int id)
        {
            ItemSolicitacao itemSolicitacao = _itemSolicitacaoDAO.BuscarDados(id);
            itemSolicitacao.Prontuario.Status = "Disponivel";
            Prontuario procurarProntuarioComDados = _prontuarioDAO.BuscarPorIdFuncionarioECaixa(itemSolicitacao.Prontuario.Id);
            _prontuarioDAO.Alterar(procurarProntuarioComDados);
            _itemSolicitacaoDAO.Remover(id);
            return RedirectToAction("Status", "Solicitacao");
        }
    }
}
