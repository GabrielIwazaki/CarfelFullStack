using System;

namespace Senai.Projeto.Carfel.Models
{
    public class ComentarioModel
    {
        public int Id { get; set; }
        public UsuarioModel Usuario { get; set; }
        public string Texto { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Status { get; set; }
    }
}