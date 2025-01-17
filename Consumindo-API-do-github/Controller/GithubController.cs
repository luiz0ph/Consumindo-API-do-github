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
                await ReturnToMenu();
            }
        }

        public async Task SearchUser(string username)
        {
            try
            {
                var res = await UserExists(username)!;
                
                if (!(res is User))
                {
                    Console.WriteLine("Usuario não encontrado na lista");
                }
                else
                {
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
                await ReturnToMenu();
            }
        }

        public async Task RepoUser(string username)
        {
            try
            {
                var user = await UserExists(username);

                if (user is User)
                {
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
                await ReturnToMenu();
            }
        }

        public async Task SumAllRepo()
        {
            try
            {
                int sum = 0;
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
                await ReturnToMenu();
            }
        }

        public async Task ListUsers()
        {
            try
            {
                Console.Clear();
                if (users.Count <= 0)
                {
                    Console.WriteLine("Nenhum usuario foi adicionado na lista ainda!");
                }
                else
                {
                    foreach (User user in users)
                    {
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
            Console.WriteLine($"Aperte qualquer tecla para voltar ao menu inicial!");
            Console.ReadKey(true);
            await Program.Menu(); // Return to menu
        }

        private async Task<User> UserExists(string username)
        {
            var res = users.Find(user => user.UserName == username);

            return res!;
        }
    }
}
