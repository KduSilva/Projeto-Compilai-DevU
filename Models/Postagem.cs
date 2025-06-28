using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilaiDevU.Models
{
    internal class Postagem
    {
        public Usuario Autor { get; set; }
        public string Codigo { get; set; }

        public Postagem (Usuario autor, string codigo)
        {
            Autor = autor;
            Codigo = codigo;
        }

        public bool PodeEditar(Usuario usuario)
        {
            return usuario == Autor || Autor.Amigos.Contains(usuario);
        }

        public override string ToString()
        {
            return $"Autor: {Autor.Nome}\nCódigo:\n{Codigo}\n---------------------";
        }
    }
}
