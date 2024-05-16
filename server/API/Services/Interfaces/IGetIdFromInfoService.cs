namespace API.Services.Interfaces
{
    public interface IGetIdFromInfoService
    {
        public Task<(string[]?, bool)> GetStationIdsByCityName(string cityName);
    }
}
