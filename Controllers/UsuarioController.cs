using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Projeto.Carfel.Models;
using Senai.Projeto.Carfel.Repositorios;

namespace Senai.Projeto.Carfel.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult Cadastro(){
            return View();
        }

        [HttpPost]
        public ActionResult Cadastro(IFormCollection form){
            
            UsuarioModel usuarioModel = new UsuarioModel();

            // linhas.Length = quantidade de linhas da minha lista
            
            if(System.IO.File.Exists("usuarios.csv")){
                string[] linhas = System.IO.File.ReadAllLines("usuarios.csv");
                usuarioModel.Id = linhas.Length + 1;
            } else {
                usuarioModel.Id = 2;
            }

            usuarioModel.Nome = form["nome"];
            usuarioModel.Email = form["email"];
            usuarioModel.Senha = form["senha"];
            
            if (!System.IO.File.Exists("usuarios.csv")) {
                using (StreamWriter sw = new StreamWriter ("usuarios.csv", true)){
                sw.WriteLine ($"{1};{"Admin"};{"admin@admin.com"};{"admin"}");
            }
            using (StreamWriter sw = new StreamWriter ("usuarios.csv", true)){
                sw.WriteLine ($"{usuarioModel.Id};{usuarioModel.Nome};{usuarioModel.Email};{usuarioModel.Senha}");
            }
            } else {
                using (StreamWriter sw = new StreamWriter ("usuarios.csv", true)){
                sw.WriteLine ($"{usuarioModel.Id};{usuarioModel.Nome};{usuarioModel.Email};{usuarioModel.Senha}");
            }
            }
            
            ViewBag.Mensagem = "Usuário Cadastrado";
            
            return RedirectToAction("Login", "Usuario");
        }

        [HttpGet]
        public ActionResult Login(){
            return View();
        }

        [HttpPost]
        public ActionResult Login(IFormCollection form){
            UsuarioModel usuario = new UsuarioModel{
                Email = form["email"],
                Senha = form["senha"]
            };

            UsuarioRepositorio usuarioRepostorio = new UsuarioRepositorio();
            UsuarioModel usuarioModel = usuarioRepostorio.BuscarPorEmailESenha(usuario.Email, usuario.Senha);

            if (usuarioModel != null){
                HttpContext.Session.SetString("idUsuario", usuarioModel.Email.ToString());
                ViewBag.Mensagem = "Login realizado com sucesso!";
                return RedirectToAction ("Cadastrar", "Comentario");
            } else {
                ViewBag.Mensagem = "Acesso negado!";
            }

            return View();

        }
    }
}