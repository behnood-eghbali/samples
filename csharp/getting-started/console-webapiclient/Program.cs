using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPIClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            var repositories = await ProcessRepositories();

            foreach (var repo in repositories)
            {
                Console.WriteLine($"Name:\t{repo.Name}");
                Console.WriteLine($"Description:\t{repo.Description}");
                Console.WriteLine($"GitHub Home Url:\t{repo.GitHubHomeUrl}");
                Console.WriteLine($"Homepage:\t{repo.Homepage}");
                Console.WriteLine($"Watchers:\t{repo.Watchers}");
                Console.WriteLine($"Forks:\t{repo.Forks}");
                Console.WriteLine($"Open Issues:\t{repo.OpenIssues}");
                Console.WriteLine($"Last Push:\t{repo.LastPush}");
                Console.WriteLine();
            }
        }

        private static async Task<List<Repository>> ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);
            return repositories;
        }
    }
}
