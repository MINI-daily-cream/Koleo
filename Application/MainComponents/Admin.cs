﻿namespace Koleo.Models
{
    public class Admin
    {
        private int ID;
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
