namespace Koleo.Models
{
    public class Admin
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        bool AddProvider()
        {
            return false;
        }
        bool EditProvider(int providerId)
        {
            return false;
        }
        bool RemoveProvider(int providerId)
        {
            return false;
        }
        List<Complaint> ListComplaints()
        {
            return new List<Complaint>();
        }
        bool AnswerComplaint(int complaintId)
        {
            return false;
        }
        AccountInfo ChceckUserAccount(int userId)
        {
            return new AccountInfo();
        }
        List<int> ListAdminCandidates()
        {
            return new List<int>();
        }
        bool AcceptNewAdmin(int userId)
        {
            return false;
        }// limit na adminow ?
        void RejectNewAdmin(int userId)
        {
            return;
        }
        void DeleteUser(int userId)
        {
            return;
        }
        bool BackupUserDB()
        {
            return false;
        }
        bool BackupProviderDB()
        {
            return false;
        }
        bool BackupConnectionDB()
        {
            return false;
        }

    }
}
