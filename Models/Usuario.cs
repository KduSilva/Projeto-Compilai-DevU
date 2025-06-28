using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilaiDevU.Models
{
    class Usuario
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<Usuario> Amigos { get; set; }

        public Usuario(string nome, string email)
        {
            Nome = nome;
            Email = email;
            Amigos = new List<Usuario>();
        }

        public void AdicionarAmigo(Usuario amigo)
        {
            if (!Amigos.Contains(amigo))
                Amigos.Add(amigo);
        }

        public override string ToString()
        {
            return $"{Nome}, ({Email})";
        }
    }
}
