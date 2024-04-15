using Application;
using Domain;
using Koleo.Models;
using Microsoft.EntityFrameworkCore;


namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Administrators { get; set; }
        public DbSet<AdminCandidate> AdminCandidates { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<CityStation> CityStations { get; set; }
        public DbSet<TicketConnection> TicketConnections { get; set; }
        public DbSet<ConnectionPoint> ConnectionPoints { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Statistics> Statistics { get; set; }
        public DbSet<AdminComplaint> AdminComplaints { get; set; }
        public DbSet<RankingUser> RankingUsers { get; set; }
        public DbSet<Ranking> Rankings { get; set; }
        public DbSet<Advertisment> Advertisments { get; set; }
        public DbSet<UserAdLink> UserAdLinks { get; set; }
    }
}
