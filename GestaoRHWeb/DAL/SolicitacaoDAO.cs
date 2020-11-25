using GestaoRHWeb.Models;

namespace GestaoRHWeb.DAL
{
    public class SolicitacaoDAO
    {
        private readonly Context _context;

        public SolicitacaoDAO(Context context) => _context = context;
    }
}
