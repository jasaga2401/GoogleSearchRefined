using System;
using System.Net.Http;
using System.Threading.Tasks;
// Add this if you're using Newtonsoft.Json
// using Newtonsoft.Json;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;

public class Item
{
    public string Title { get; set; }
    public string Link { get; set; }
    // ... other properties if any
}

public class SearchResult
{
    [JsonProperty("items")]
    public List<Item> Items { get; set; }
    // ... other properties if needed
}
class Program
{
    static async Task Main(string[] args)
    {
        string apiKey = "<API Key>"; // Replace with your actual API key
        string searchEngineId = "<Search Engine Id>"; // Replace with your actual Search Engine ID
        string query = "recipes for christmas";

        int numberOfResults = 5;

        HttpClient httpClient = new HttpClient();
        Item item1 = new Item();

        try
        {
            string encodedQuery = WebUtility.UrlEncode(query); // Corrected here
            string apiUrl = $"https://www.googleapis.com/customsearch/v1?key={apiKey}&cx={searchEngineId}&q={encodedQuery}&num={numberOfResults}";

            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                // var searchResult = JsonSerializer.Deserialize<SearchResult>(responseBody); // Ensure consistent use of JsonSerializer
                var searchResult = JsonConvert.DeserializeObject<SearchResult>(responseBody);

                foreach (var item in searchResult.Items)
                {
                    Console.WriteLine("Title: " + item.Title);
                    Console.WriteLine("Link: " + item.Link);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine($"Failed to get a response. Status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            httpClient.Dispose();
        }

        Console.ReadLine();
    }

    // Your SearchResult, SearchInformation, and Item classes go here
}