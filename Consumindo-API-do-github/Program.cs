using Consumindo_API_do_github.Controller;
using Consumindo_API_do_github.Model;
using System.Net.Http;
using System.Text.Json;

namespace Consumindo_API_do_github
{
    internal class Program
    {
        private static readonly GithubController controller = new GithubController();

        static async Task Main(string[] args)
        {
            await Menu();
        }

        public static async Task Menu()
        {
            #region Options menu
            Console.Clear();
            Console.WriteLine($"Escolha uma das opções abaixo: \n" +
                $"1 - Adicionar usuario na lista \n" +
                $"2 - Procurar usuario na lista \n" +
                $"3 - Listar repositorios de um usuario \n" +
                $"4 - Somar todos repositorios \n" +
                $"5 - Listar todos os usuarios \n" +
                $"6 - Sair \n");
            #endregion

            // User choice
            Console.Write("Escolha: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        await controller.AddUser(await GetUsername());
                        break;
                    case 2:
                        await controller.SearchUser(await GetUsername());
                        break;
                    case 3:
                        await controller.RepoUser(await GetUsername());
                        break;
                    case 4:
                        await controller.SumAllRepo();
                        break;
                    case 5:
                        await controller.ListUsers();
                        break;
                    case 6:
                        Console.WriteLine("Saindo....");
                        break;
                    default:
                        Console.WriteLine("Opção invalida!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida. Por favor, insira um número.");
                await Menu();
            }
        }

        public static async Task<string> GetUsername()
        {
            Console.Write("Nome do usuario: ");
            string username = Console.ReadLine()!;
            return username;
        }
    }
}
