namespace API.Services.Interfaces
{
    public interface IDatabaseServiceAPI
    {
        Task<(List<string[]>, bool)> ExecuteSQL(string sql);
        Task<(List<object[]>, bool)> ExecuteSQLLastRow(string sql);
        /// ------
        Task<bool> SaveConnectionSeatInfo(string Connection_Id, string seatText);
    }
}
