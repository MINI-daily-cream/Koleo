using API.Services.Interfaces;
using Application;

namespace API.Services
{
    public class GetIdFromInfoService : IGetIdFromInfoService
    {
        private readonly IDatabaseServiceAPI _databaseService;
        public GetIdFromInfoService(IDatabaseServiceAPI databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<(string?, bool)> GetStationIdByName(string stationName)
        {
            string sql = $"SELECT Id FROM Stations WHERE Name='{stationName}'";
            var result = await _databaseService.ExecuteSQL(sql);
            if (!result.Item2) return (null, false);
            return (result.Item1[0][0], true);
        }

        public async Task<(string?, bool)> GetCityIdByName(string cityName)
        {
            string sql = $"SELECT Id FROM Cities WHERE Name='{cityName}'";
            var result = await _databaseService.ExecuteSQL(sql);
            if (!result.Item2) return (null, false);
            return (result.Item1[0][0], true);
        }

        public async Task<(List<string>?, bool)> GetStationIdsByCityName(string cityName)
        {
            //string sql = $"SELECT s.Id FROM Stations s JOIN  WHERE Name='{cityName}'";
            var cityIdResult = await GetCityIdByName(cityName);
            if (!cityIdResult.Item2) return (null, false);
            string cityId = cityIdResult.Item1;

            string sql = $"SELECT s.Id FROM Stations s JOIN CityStations cs ON s.Id = cs.Station_Id WHERE cs.City_Id ='{cityId}'";
            var result = await _databaseService.ExecuteSQL(sql);
            if (!result.Item2) return (null, false);

            List<string> stationIds = new List<string>();
            
            foreach (string[] idArr in result.Item1)
            {
                stationIds.Add(idArr[0]);
            }

            return (stationIds, true);
        }
    }
}
