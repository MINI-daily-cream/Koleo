namespace Koleo.Models
{
    public class Advertisment
    {
        public Guid Id { get; set; }
        public string AdContent { get; set; }
        public string AdLinkUrl { get; set; }
        public string AdImageUrl { get; set; }
        public AdvertismentCategory AdCategory { get; set; }
        public string AdOwner { get; set; }
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
