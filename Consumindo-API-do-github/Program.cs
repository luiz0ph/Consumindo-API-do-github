using Consumindo_API_do_github.Model;

namespace Consumindo_API_do_github
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // User list
            List<User> users = new List<User>();
            Menu();
        }

        public static void Menu()
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
            int choice = int.Parse(Console.ReadLine()!);
        }
    }
}
