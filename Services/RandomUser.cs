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

    public async Task<User> FetchRandomUserDataAsync()
{
    string url = "https://randomuser.me/api/";

    var response = await _httpClient.GetFromJsonAsync<RandomUserReturn>(url);
    
    if (response?.Results != null && response.Results.Length > 0)
    {
        var randomUser = response.Results[0];
        
        var user = new User
        {
            Name = $"{randomUser.Name.First} {randomUser.Name.Last}",
            Email = randomUser.Email,
            Phone = randomUser.Phone,
            Address = $"{randomUser.Location.City}, {randomUser.Location.Country}",
            PictureUrl = randomUser.Picture.Large
        };

        return user;
    }

    return null;
}

}
