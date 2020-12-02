using GestaoRHWeb.DAL;
using GestaoRHWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GestaoRHWeb.Controllers
{
    [Route("api/Funcionario")]
    [ApiController]
    public class FuncionarioAPIController : ControllerBase
    {
        private readonly FuncionarioDAO _funcionarioDAO;

        public FuncionarioAPIController(FuncionarioDAO funcionarioDAO)
        {
            _funcionarioDAO = funcionarioDAO;
        }

        //GET: /api/Funcionario/Listar
        [HttpGet]
        [Route("Listar")]
        public IActionResult Listar()
        {
            List<Funcionario> funcionarios = _funcionarioDAO.Listar();
            if (funcionarios.Count > 0)
            {
                return Ok(funcionarios);
            }
            return BadRequest(new { msg = "Não existem registros de funcionários" });
        }

        //GET: /api/Funcionario/Buscar
        [HttpGet]
        [Route("Buscar/{id}")]
        public IActionResult Buscar(int id)
        {
            Funcionario funcionario = _funcionarioDAO.BuscarPorId(id);
            if (funcionario != null)
            {
                return Ok(funcionario);
            }
            return NotFound(new { msg = "Funcionario não encontrado!" });
        }

        //GET: /api/Funcionario/Cadastrar
        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult Cadastrar(Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                if (_funcionarioDAO.Cadastrar(funcionario))
                {
                    return Created("", funcionario);
                }
                return Conflict(new { msg = "O funcionário já está cadastrado!" });
            }
            return BadRequest(ModelState);
        }

    }
}
