using System;
using System.Collections.Generic;
using System.IO;
using Senai.Projeto.Carfel.Models;

namespace Senai.Projeto.Carfel.Repositorios
{
    public class UsuarioRepositorio
    {
        private List<UsuarioModel> CarregarDoCSV(){
            List<UsuarioModel> lsUsuario = new List<UsuarioModel>();
            string[] linhas = File.ReadAllLines("usuarios.csv");
            
            foreach (string linha in linhas){
                string[] dadosDaLinha = linha.Split(';');
                UsuarioModel usuario = new UsuarioModel{
                    Nome = dadosDaLinha [1],
                    Email = dadosDaLinha [2],
                    Senha = dadosDaLinha [3],
                    
                };

                lsUsuario.Add(usuario);
            }
            return lsUsuario;
        }

        public UsuarioModel BuscarPorEmailESenha(string email, string senha){
            List<UsuarioModel> usuariosCadastrados = CarregarDoCSV();
            foreach (UsuarioModel usuario in usuariosCadastrados){
                if (usuario.Email == email && usuario.Senha == senha){
                    return usuario;
                }
            }

            return null;
        }

    }
}