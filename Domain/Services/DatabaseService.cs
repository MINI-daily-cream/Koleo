using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Reflection.PortableExecutable;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Koleo.Models
{
    public static class DatabaseService
    {
        //public static IConfiguration Configuration { get; }
        //public DatabaseService(IConfiguration configuration)
        //{
          //  Configuration = configuration;
        //}
        public static async Task<List<string[]>> ExecuteSQL(string sql)
        {
            var results = new List<string[]>();

            await using var conn = new SqliteConnection("Data Source =./koleo.db");

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
        public static async void Backup()
        {
            string backupDb = "KoleoBackup.db";

            var backup = new SqliteConnection($"Data Source={backupDb}");

            await using var conn = new SqliteConnection("Data Source=./koleo.db");
            await conn.OpenAsync();

            conn.BackupDatabase(backup);
        }
    }
}