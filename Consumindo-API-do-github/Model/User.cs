using System.Text.Json.Serialization;

namespace Consumindo_API_do_github.Model
{
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("login")]
        public string? UserName { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("bio")]
        public string? Bio { get; set; }

        [JsonPropertyName("public_repos")]
        public int PublicRepos { get; set; }

        [JsonPropertyName("repos_url")]
        public string ReposUrl { get; set; }
    }
}
