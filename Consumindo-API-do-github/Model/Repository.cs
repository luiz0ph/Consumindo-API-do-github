using System.Text.Json.Serialization;

namespace Consumindo_API_do_github.Model
{
    public class Repository
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName ("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("forks")]
        public int Forks { get; set; }

        [JsonPropertyName("stargazers_count")]
        public int Stars { get; set; }
    }
}
