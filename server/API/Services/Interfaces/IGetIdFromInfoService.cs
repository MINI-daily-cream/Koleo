namespace API.Services.Interfaces
{
    public interface IGetIdFromInfoService
    {
        public Task<(List<string>?, bool)> GetStationIdsByCityName(string cityName);
    }
}
