namespace API.Services.Interfaces
{
    public interface IDatabaseServiceAPI
    {
        Task<List<string[]>> ExecuteSQL(string sql);
        Task<(List<object[]>, bool)> ExecuteSQLLastRow(string sql);
    }
}
