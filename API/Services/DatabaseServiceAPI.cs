using API.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.Reflection.PortableExecutable;

namespace KoleoPL.Services
{
    public class DatabaseServiceAPI : IDatabaseServiceAPI
    {
        public IConfiguration Configuration { get; }
        public DatabaseServiceAPI(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async Task<List<string[]>> ExecuteSQL(string sql)
        {
            var results = new List<string[]>();

            await using var conn = new SqliteConnection(Configuration.GetConnectionString("DefaultConnection"));

            await conn.OpenAsync();

            await using var cmd = new SqliteCommand(sql, conn);

            await using var dataReader = await cmd.ExecuteReaderAsync();

            while(await dataReader.ReadAsync())
            {
                //Console.WriteLine(dataReader["Name"]);
                //Console.WriteLine(dataReader["Surname"]);

                var values = new string[dataReader.FieldCount];
                dataReader.GetValues(values);
                results.Add(values);
            }
            
              
            return results;
        }
        public async void Backup()
        {
            string backupDb = "KoleoBackup.db";

            var backup = new SqliteConnection($"Data Source={backupDb}");

            await using var conn = new SqliteConnection(Configuration.GetConnectionString("DefaultConnection"));
            await conn.OpenAsync();

            conn.BackupDatabase(backup);
        }
    }
}