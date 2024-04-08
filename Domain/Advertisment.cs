namespace Koleo.Models
{
    public class Advertisment
    {
        public Guid Id { get; set; }
        public string AdContent { get; set; }
        public string AdLink { get; set; }
        public byte[] AdImage { get; set; }
        public AdvertismentCategory AdCategory { get; set; }
    }

    public enum AdvertismentCategory
    {
        Health,
        Clouth,
        Games,
        Sport,
        Travel,
        Animals,
        Food
    }
}
