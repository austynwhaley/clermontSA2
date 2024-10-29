using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class RandomUser
{
    private readonly HttpClient _httpClient;

    public RandomUser(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<RandomUserReturn> FetchRandomUserDataAsync()
    {
        // The URL for the random user API
        string url = "https://randomuser.me/api/";

        // Fetch and deserialize JSON data into the Return model
        var response = await _httpClient.GetFromJsonAsync<RandomUserReturn>(url);
        
        return response;
    }
}
