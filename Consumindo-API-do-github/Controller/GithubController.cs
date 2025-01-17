using Consumindo_API_do_github.Model;
using System.Net.Http;
using System.Text.Json;

namespace Consumindo_API_do_github.Controller
{
    public class GithubController
    {
        private readonly HttpClient client = new HttpClient();
        // User list
        List<User> users = new List<User>();

        public async Task AddUser(string username)
        {
            try
            {
                string url = $"https://api.github.com/users/{username}";

                // Request headers
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");

                // Request API
                var res = await client.GetStringAsync(url);
                var data = JsonSerializer.Deserialize<User>(res);

                // If the response is non-null
                if (data != null)
                {
                    
                    if (false)
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
                await ReturnToMenu();
            }
        }

        public async Task<User>? SearchUser(string username)
        {
            try
            {
                var user = users.Where(user => user.UserName == username).ToList();

                if (user.Count <= 0)
                {
                    return null;
                }
                else
                {
                    await ShowUser(user[0]);
                    return user[0];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex}");
                throw ex;
            }
            finally
            {
                await ReturnToMenu();
            }
        }

        private async Task ShowUser(User user)
        {
            Console.WriteLine($"Id - {user.Id} \n" +
                $"Username - {user.UserName} \n" +
                $"Name - {user.Name} \n" +
                $"Bio - {user.Bio} \n");
        }

        private async Task ReturnToMenu()
        {
            Console.WriteLine($"Aperte qualquer botão para voltar ao menu inicial!");
            Console.ReadKey(true);
            await Program.Menu(); // Return to menu
        }
    }
}
