namespace API.Services.Interfaces
{
    public interface IDatabaseServiceAPI
    {
        Task<(List<string[]>, bool)> ExecuteSQL(string sql);
    }
}
