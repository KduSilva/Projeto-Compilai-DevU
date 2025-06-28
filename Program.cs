using System;
using System.Collections.Generic;
using CompilaiDevU.Models;

namespace CompilaiDevU
{
    class Program
    {
        // Listas de usuários cadastrados
        static List<Usuario> usuarios = new List<Usuario>();

        // Listas de postagens criadas
        static List<Postagem> postagens = new List<Postagem>();



        static void Main()
        {
            // Mensagem inicial com nome da plataforma
            Console.WriteLine("====================================");
            Console.WriteLine("   Bem-vindo ao Compilaí DevU ");
            Console.WriteLine(" Conecta, Codifica, Compartilha  ");
            Console.WriteLine("====================================");

            int opcao;

            // Menu principal do sistema
            do
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1 - Cadastrar novo compilante");
                Console.WriteLine("2 - Adicionar amigo");
                Console.WriteLine("3 - Criar nova postagem de código");
                Console.WriteLine("4 - Ver feed de postagens");
                Console.WriteLine("5 - Editar código (se permitido)");
                Console.WriteLine("0 - Sair");

                Console.Write("Escolha uma opção: ");
                opcao = int.Parse(Console.ReadLine());


                // Executa a ação escolhida
                switch (opcao)
                {
                    case 1: CadastrarUsuario(); break;
                    case 2: AdicionarAmizade(); break;
                    case 3: CriarPostagem(); break;
                    case 4: VerFeed(); break;
                    case 5: EditarPostagem(); break;
                    case 0: Console.WriteLine("Até logo, compilante! "); break;
                    default: Console.WriteLine("Opção inválida."); break;
                }

            } while (opcao != 0); // Repete as opçoes ate o usuario decidir sair
        }

        // Aqui virão os métodos CadastrarUsuario, AdicionarAmizade, etc...

        // Cadastra um novo usuário na plataforma
        static void CadastrarUsuario()
        {
            Console.WriteLine("Digite o nome do compilante: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Digite o e-mail do compilante: ");
            string email = Console.ReadLine();

            // Cria um novo objeto do tipo Usuario
            Usuario novoUsuario = new Usuario(nome, email);

            // Adiciona na lista global de usuários
            usuarios.Add(novoUsuario);

            Console.WriteLine("Cadastro realizado com sucesso! ");

        }

        // Cria amizade entre dois usuários
        static void AdicionarAmizade()
        {
            Console.WriteLine("Digite o e-mail do compilante que quer adicionar como amigo: ");
            string emailOrigem = Console.ReadLine();

            // Busca um usuário que vai adicionar como amigo
            Usuario origem = usuarios.Find(u => u.Email == emailOrigem);
            if (origem == null)
            {
                Console.WriteLine("Usuário não encontrado. Verifique o e-mail.");
                return;
            }

            Console.WriteLine("Digite o e-mail do amigo que deseja adicionar: ");
            string emailDestino = Console.ReadLine();

            // Busca o usuário que será adicionado como amigo
            Usuario destino = usuarios.Find(u => u.Email == emailDestino);
            if (destino == null)
            {
                Console.WriteLine("Amigo não encontrado.");
                return;
            }
            // Evita adicionar a si mesmo ou duplicar amizades
            if (origem == destino)
            {
                Console.WriteLine("Você não pode adicionar a si mesmo como amigo.");
                return;
            }
            origem.AdicionarAmigo(destino);
            destino.AdicionarAmigo(origem); // Adiciona a amizade na outra direção também
            Console.WriteLine($"{destino.Nome} agora é amigo de compilação de {origem.Nome}! ");
        }

        // Cria uma nova postagem de código
        static void CriarPostagem()
        {
            Console.WriteLine("Digite o e-mail do compilante que vai postar: ");
            string email = Console.ReadLine();

            // Busca o autor da postagem
            Usuario autor = usuarios.Find(u => u.Email == email);
            if (autor == null)
            {
                Console.WriteLine("Usuário não encontrado.");
                return;
            }
            Console.WriteLine("Digite o código a ser postado: ");
            string codigo = Console.ReadLine();

            // Cria uma postagem com autor e código
            Postagem novaPostagem = new Postagem(autor, codigo);

            // Adiciona na lista global de postagens
            postagens.Add(novaPostagem);
            Console.WriteLine("Código postado com sucesso no Compilaí DevU! ");
        }

        // Exibe todas as postagens no feed
        static void VerFeed()
        {
            if (postagens.Count == 0)
            {
                Console.WriteLine("Nenhuma postagem encontrada. 💤");
                return;
            }

            Console.WriteLine("\n📢 Feed do Compilaí DevU:");
            foreach (var postagem in postagens)
            {
                Console.WriteLine(postagem.ToString());
            }
        }

        // Permite editar uma postagem, se o usuário tiver permissão
        static void EditarPostagem()
        {
            Console.Write("Digite o e-mail do compilante que deseja editar um código: ");
            string emailEditor = Console.ReadLine();

            // Busca o usuário que quer editar
            Usuario editor = usuarios.Find(u => u.Email == emailEditor);
            if (editor == null)
            {
                Console.WriteLine("Usuário não encontrado.");
                return;
            }

            // Mostra todas as postagens com índice
            Console.WriteLine("Selecione a postagem que deseja editar:");
            for (int i = 0; i < postagens.Count; i++)
            {
                Console.WriteLine($"{i} - Autor: {postagens[i].Autor.Nome}");
            }

            Console.Write("Digite o número da postagem: ");
            if (!int.TryParse(Console.ReadLine(), out int indice) || indice < 0 || indice >= postagens.Count)
            {
                Console.WriteLine("Índice inválido.");
                return;
            }

            Postagem p = postagens[indice];

            // Verifica permissão de edição
            if (p.PodeEditar(editor))
            {
                Console.WriteLine("Digite o novo código:");
                string novoCodigo = Console.ReadLine();
                p.Codigo = novoCodigo;

                Console.WriteLine("Código atualizado com sucesso! ");
            }
            else
            {
                Console.WriteLine("Você não tem permissão para editar esta postagem. ");
            }
        }
    }
}