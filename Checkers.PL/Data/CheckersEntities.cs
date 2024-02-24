using Microsoft.EntityFrameworkCore;
using Checkers.PL.Entities;

namespace Checkers.PL.Data
{
    public class CheckersEntities : DbContext
    {
        Guid[] userId = new Guid[3];
        Guid[] gameId = new Guid[4];
        Guid[] gameStateId = new Guid[5];
        Guid[] userGameId = new Guid[3];

        public virtual DbSet<tblGame> tblGames { get; set; }
        public virtual DbSet<tblGameState> tblGameStates { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
        public virtual DbSet<tblUserGame> tblUserGames { get; set; }

        public CheckersEntities(DbContextOptions<CheckersEntities> options) : base(options)
        {


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public CheckersEntities()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            //CreateUsers(modelBuilder);
            //CreateGenres(modelBuilder);
            //CreateFormats(modelBuilder);
            //CreateCustomers(modelBuilder);
            //CreateDirectors(modelBuilder);
            //CreateRatings(modelBuilder);
            //CreateMovies(modelBuilder);
            //CreateMovieGenres(modelBuilder);
            //CreateOrders(modelBuilder);
            //CreateOrderItems(modelBuilder);
            //CreateCarts(modelBuilder);
            //CreateCartItems(modelBuilder);
        }

        private static string GetHash(string Password)
        {
            using (var hasher = new System.Security.Cryptography.SHA1Managed())
            {
                var hashbytes = System.Text.Encoding.UTF8.GetBytes(Password);
                return Convert.ToBase64String(hasher.ComputeHash(hashbytes));
            }
        }
    }
}
