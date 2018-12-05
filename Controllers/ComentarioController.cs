using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Projeto.Carfel.Models;

namespace Senai.Projeto.Carfel.Controllers {
    public class ComentarioController : Controller {
        [HttpGet]
        public IActionResult Cadastrar () {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("idUsuario"))){
                return RedirectToAction("Login", "Usuario");
            }
            return View ();
        }

        [HttpPost]
        public IActionResult Cadastrar(IFormCollection form){
            ComentarioModel comentario = new ComentarioModel();
            comentario.Id = 1;
            comentario.Texto = form["comentario"];
            comentario.DataCriacao = DateTime.Now;

            using(StreamWriter sw = new StreamWriter("comentarios.csv", true)){
                sw.WriteLine($"{comentario.Usuario};{comentario.Texto};{comentario.DataCriacao}");
            }

            ViewBag.Mensagem = "Comentário aguardando por verificação";

            return View();

        }
    }
}