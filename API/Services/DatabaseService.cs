using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.Reflection.PortableExecutable;

namespace KoleoPL.Services
{
    public class DatabaseService
    {
        public IConfiguration Configuration { get; }
        public DatabaseService(IConfiguration configuration)
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
            string backupPath = "C:\\KoleoBackup.bak",
                sql = $"VACUUM INTO '{backupPath}'";
                //sql = $"BACKUP DATABASE YourDatabase TO DISK = '{backupPath}'";

            //using (var location = new SqliteConnection(Configuration.GetConnectionString("DefaultConnection")))
            //using (var destination = new SqliteConnection(string.Format($"Data Source={backupPath};")))
            //{
            //    location.Open();
            //    destination.Open();
            //    location.BackupDatabase(destination, "main", "main", -1, null, 0);
            //}

            

            //string connectionString = "Data Source=YourServer;Initial Catalog=YourDatabase;Integrated Security=True";

            await using var conn = new SqliteConnection(Configuration.GetConnectionString("DefaultConnection"));

            await conn.OpenAsync();

            await using var cmd = new SqliteCommand(sql, conn);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}