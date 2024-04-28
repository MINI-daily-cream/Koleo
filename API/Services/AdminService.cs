using API.Services.Interfaces;

namespace Koleo.Services
{
    public class AdminService
    {
        private readonly IDatabaseServiceAPI _databaseService;
        public AdminService(IDatabaseServiceAPI databaseService) // jakieś DI
        {
            _databaseService = databaseService;
        }

        public void CreateAdminAccount()
        {

        }

        public void VerifyAdminAccount()
        {

        }

        public void AuthoriseAdmin()
        {

        }

        public void RemoveAdminAccount()
        {

        }

        public void GiveAdminPermissions()
        {

        }
    }
}
