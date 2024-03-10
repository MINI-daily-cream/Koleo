namespace Koleo.Models
{
    public class Admin
    {
        private int ID;
        bool AddProvider()
        bool EditProvider(int providerId) {}
        bool RemoveProvider(int providerId) { }
        List<Complaint> ListComplaints() { }
        bool AnswerComplaint(int complaintId) { }
        AccountInfo ChceckUserAccount(int userId) { }
        List<int> ListAdminCandidates() { }
        bool AcceptNewAdmin(int userId) { } // limit na adminow ?
        void RejectNewAdmin(int userId) { }
        void DeleteUser(int userId) { }
        bool BackupUserDB() { }
        bool BackupProviderDB() { }
        bool BackupConnectionDB() { }

    }
}
