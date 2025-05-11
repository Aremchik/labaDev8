public interface IDataService
{
    Task<UserData> ProcessUserData(UserInput input);
}