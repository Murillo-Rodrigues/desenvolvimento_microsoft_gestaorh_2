﻿using GestaoRHWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GestaoRHWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsuarioController(Context context, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Usuario/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Senha,Id,CriadoEm,ConfirmacaoSenha,Cep,Logradouro,Bairro,Cidade,Uf")] UsuarioView usuarioView)
        {
            if (ModelState.IsValid)
            {

                Usuario usuario = new Usuario
                {
                    UserName = usuarioView.Email,
                    Email = usuarioView.Email,
                };

                IdentityResult resultado = await _userManager.CreateAsync(usuario, usuarioView.Senha);

                if (resultado.Succeeded)
                {

                    _context.Add(usuarioView);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                AdiconarErros(resultado);
            }
            return View(usuarioView);
        }

        public void AdiconarErros(IdentityResult resultado)
        {
            foreach (IdentityError erro in resultado.Errors)
            {
                ModelState.AddModelError("", erro.Description);
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email, Senha")] UsuarioView usuarioView)
        {

            var result = await _signInManager.PasswordSignInAsync(usuarioView.Email, usuarioView.Senha, false, false);

            string name = User.Identity.Name;
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Solicitacao");
            }
            ModelState.AddModelError("", "Login ou senha inválidos!");
            return View(usuarioView);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Funcionario");
        }


    }
}
