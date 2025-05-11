public class DataService : IDataService
{
    private readonly AppDbContext _db;
    private readonly IHttpClientFactory _httpClientFactory;

    public DataService(AppDbContext db, IHttpClientFactory httpClientFactory)
    {
        _db = db;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<UserData> ProcessUserData(UserInput input)
    {
    
        var userData = new UserData
        {
            Name = input.Name,
            Email = input.Email
        };

        _db.UserData.Add(userData);
        await _db.SaveChangesAsync();

        var client = _httpClientFactory.CreateClient();

        try
        {
            var response = await client.PostAsJsonAsync("http://python_service:8000/raw_user/", input);
            response.EnsureSuccessStatusCode(); 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при отправке в Python-сервис: {ex.Message}");
        }

        return userData;
    }
}
