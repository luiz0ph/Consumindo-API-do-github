using Consumindo_API_do_github.Model;
using System.Net.Http;
using System.Runtime.Intrinsics.Arm;
using System.Text.Json;

namespace Consumindo_API_do_github.Controller
{
    public class GithubController
    {
        private readonly HttpClient client = new HttpClient();
        // User list
        List<User> users = new List<User>();

        // Method to add a user to the list
        public async Task AddUser(string username)
        {
            try
            {
                // url for request
                string url = $"https://api.github.com/users/{username}";

                // Request headers
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");

                // Request API
                var res = await client.GetStringAsync(url);
                var data = JsonSerializer.Deserialize<User>(res);

                // If the response is non-null
                if (data != null)
                {
                    // Check if the user exists in the list
                    var result = await UserExists(username);
                    if (result is User)
                    {
                        Console.WriteLine($"O usuario {username} já foi adicionado na lista!");
                        return;
                    }
                    else
                    {
                        users.Add(data); // Add user
                        Console.WriteLine($"O usuario {username} foi adicionado na lista!");
                    }
                }
                else
                {
                    // If the answer is null
                    Console.WriteLine($"Não foi possivel adicionar o usuario {username} na lista!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw ex;
            }
            finally
            {
                // Return to main menu
                await ReturnToMenu();
            }
        }

        // Method to search for user in list
        public async Task SearchUser(string username)
        {
            try
            {
                // Check if the user exists in the list
                var res = await UserExists(username)!;

                // If the user is already in the list
                if (!(res is User))
                {
                    Console.WriteLine("Usuario não encontrado na lista");
                }
                else
                {
                    // Print user information
                    await ShowUser(res);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex}");
                throw ex;
            }
            finally
            {
                // Return to main menu
                await ReturnToMenu();
            }
        }

        // Method to list a user's top 3 repositories
        public async Task RepoUser(string username)
        {
            try
            {
                // Check if the user exists in the list
                var user = await UserExists(username);

                if (user is User)
                {
                    // Show user's top 3 repositories

                    Console.Clear();
                    Console.WriteLine(user.UserName);
                    Console.WriteLine("Repositorios: ");

                    var res = await client.GetStringAsync(user.ReposUrl);
                    List<Repository> repository = JsonSerializer.Deserialize<List<Repository>>(res);

                    for (int i = 0; i < Math.Min(3, repository.Count); i++)
                    {
                        Console.WriteLine(" ");
                        Console.WriteLine($"Repositorio: {repository[i].Name} \n" +
                            $"Descrição: {repository[i].Description} \n" +
                            $"Forks: {repository[i].Forks} \n" +
                            $"Estrelas: {repository[i].Stars} \n");
                        Console.WriteLine("-------------------------------------------------");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw ex;
            }
            finally
            {
                // Return to main menu
                await ReturnToMenu();
            }
        }

        // Method to add all repositories of users in the list
        public async Task SumAllRepo()
        {
            try
            {
                // Initialize the sum variable to 0
                int sum = 0;

                // For each user in the list
                foreach (User user in users)
                {
                    sum += user.PublicRepos;
                }
                Console.WriteLine($"A soma de todos os repositorios: {sum}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                throw ex;
            }
            finally
            {
                // Return to main menu
                await ReturnToMenu();
            }
        }

        // Method to list all users
        public async Task ListUsers()
        {
            try
            {
                Console.Clear();

                // If no user has been added to the list
                if (users.Count <= 0)
                {
                    Console.WriteLine("Nenhum usuario foi adicionado na lista ainda!");
                }
                else
                {
                    // Display each user in the list
                    foreach (User user in users)
                    {
                        // Print user information
                        await ShowUser(user);
                        Console.WriteLine("---------------------------------------");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex}");
                throw ex;
            }
            finally
            {
                // Return to main menu
                await ReturnToMenu();
            }
        }

        // Method to display user information
        private async Task ShowUser(User user)
        {
            Console.WriteLine($"Id - {user.Id} \n" +
                $"Username - {user.UserName} \n" +
                $"Name - {user.Name} \n" +
                $"Bio - {user.Bio} \n");
        }

        // Method to return to the main menu
        private async Task ReturnToMenu()
        {
            Console.WriteLine($"Aperte qualquer tecla para voltar ao menu inicial!");
            Console.ReadKey(true);
            await Program.Menu(); // Return to menu
        }

        // Method to check if a user exists
        private async Task<User> UserExists(string username)
        {
            var res = users.Find(user => user.UserName == username);

            return res!;
        }
    }
}