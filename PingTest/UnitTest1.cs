using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string url = "http://host.docker.internal:8081"; // Replace with the website you want to ping
        await PingWebsite(url);
    }

    static async Task PingWebsite(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Ping to {url} successful. Status Code: {response.StatusCode}");
                }
                else
                {
                    Console.WriteLine($"Ping to {url} failed. Status Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error pinging {url}: {ex.Message}");
            }
        }
    }
}
