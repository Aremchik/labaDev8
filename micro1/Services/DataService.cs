public interface IDataService
{
    Task<UserData> ProcessUserData(UserInput input);
}

public class DataService : IDataService
{
    private readonly AppDbContext _db;
    private readonly HttpClient _httpClient;
    private readonly string _pythonServiceUrl;

    public DataService(AppDbContext db, IConfiguration config)
    {
        _db = db;
        _httpClient = new HttpClient();
        _pythonServiceUrl = config["PYTHON_SERVICE_URL"];
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

        // Отправка в Python сервис
        var processedData = new
        {
            original_name = input.Name,
            processed_name = input.Name.ToUpper(),
            email = input.Email,
            timestamp = DateTime.UtcNow
        };

        var response = await _httpClient.PostAsJsonAsync(
            $"{_pythonServiceUrl}/process", 
            processedData);
        response.EnsureSuccessStatusCode();

        return userData;
    }
}