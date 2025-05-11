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
        
        return userData;
    }
}