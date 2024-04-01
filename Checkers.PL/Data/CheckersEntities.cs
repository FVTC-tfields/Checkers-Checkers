using Checkers.PL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Checkers.PL.Data
{
    public class CheckersEntities : DbContext
    {
        Guid[] userId = new Guid[4];
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

            CreateUsers(modelBuilder);
            CreateGameStates(modelBuilder);
            CreateGames(modelBuilder);           
            CreateUserGames(modelBuilder);
        }

        private void CreateGameStates(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < gameStateId.Length; i++)
                gameStateId[i] = Guid.NewGuid();

            modelBuilder.Entity<tblGameState>( entity => {
                entity.HasKey(e => e.Id).HasName("PK_tblGameState_Id");

                entity.ToTable("tblGameState");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Row)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Column)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.IsKing).IsRequired().IsUnicode(false);
            });

            List<tblGameState> GameStates = new List<tblGameState>
            {
                new tblGameState {Id = gameStateId[0], Row = "1", Column = "4", IsKing = true},
                new tblGameState {Id = gameStateId[1], Row = "5", Column = "4", IsKing = false},
                new tblGameState {Id = gameStateId[2], Row = "2", Column = "8", IsKing = false},
                new tblGameState {Id = gameStateId[3], Row = "1", Column = "10", IsKing = false},
                new tblGameState {Id = gameStateId[4], Row = "7", Column = "1", IsKing = true}

            };
            modelBuilder.Entity<tblGameState>().HasData(GameStates);
        }

        private void CreateUserGames(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < userGameId.Length; i++)
                userGameId[i] = Guid.NewGuid();

            modelBuilder.Entity<tblUserGame>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblUserGame_Id");

                entity.ToTable("tblUserGame");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                //entity.HasOne(d => d.User)
                //    .WithMany(p => p.tblUserGames)
                //    .HasForeignKey(d => d.UserId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("fk_tblUserGame_UserId");

                //entity.HasOne(d => d.Game)
                //    .WithMany(p => p.tblUserGames)
                //    .HasForeignKey(d => d.GameId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("fk_tblUserGame_GameId");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.GameId)
                    .IsRequired()
                    .IsUnicode(false);

            });

            List<tblUserGame> UserGames = new List<tblUserGame>
            {
                new tblUserGame {Id = userGameId[0], Color = "Red", GameId = gameId[0], UserId = userId[0]},
                new tblUserGame {Id = userGameId[1], Color = "Black", GameId = gameId[0], UserId = userId[1]},
                new tblUserGame {Id = userGameId[2], Color = "Red", GameId = gameId[0], UserId = userId[2]}
            };
            modelBuilder.Entity<tblGame>().HasData(UserGames);
        }

        private void CreateGames(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < gameId.Length; i++)
                gameId[i] = Guid.NewGuid();

            modelBuilder.Entity<tblGame>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblGame_Id");

                entity.ToTable("tblGame");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Winner)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.GameDate).HasColumnType("datetime");

                entity.Property(e => e.GameStateId)
                    .IsRequired()
                    .IsUnicode(false);

                //entity.HasOne(d => d.GameState)
                //    .WithMany(p => p.tblGames)
                //    .HasForeignKey(d => d.GameStateId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("fk_tblGame_GameStateId");

            });

            List<tblGame> Games = new List<tblGame>
            {
                new tblGame {Id = gameId[0], Name = "Example", GameDate = DateTime.Now, GameStateId = gameStateId[0], Winner = null},
                new tblGame {Id = gameId[1], Name = "George", GameDate = DateTime.Now, GameStateId = gameStateId[1], Winner = null},
                new tblGame {Id = gameId[2], Name = "Hanna", GameDate = DateTime.Now, GameStateId = gameStateId[2], Winner = null},
                new tblGame {Id = gameId[3], Name = "World War 42", GameDate = DateTime.Now, GameStateId = gameStateId[4], Winner = "MetalWhee3l"}
            };
            modelBuilder.Entity<tblGame>().HasData(Games);
        }

        private void CreateUsers(ModelBuilder modelBuilder)
        {
            for (int i = 0; i < userId.Length; i++)
            {
                userId[i] = Guid.NewGuid();
            }

            modelBuilder.Entity<tblUser>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_tblUser_Id");

                entity.ToTable("tblUser");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(28)
                    .IsUnicode(false);
                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tblUser>().HasData(new tblUser
            {
                Id = userId[0],
                FirstName = "Barney",
                LastName = "Smith",
                UserName = "MetalWhee3l",
                Password = GetHash("maple")
            });
            modelBuilder.Entity<tblUser>().HasData(new tblUser
            {
                Id = userId[1],
                FirstName = "John",
                LastName = "Doro",
                UserName = "jdoro",
                Password = GetHash("maple")
            });
            modelBuilder.Entity<tblUser>().HasData(new tblUser
            {
                Id = userId[2],
                FirstName = "Brian",
                LastName = "Foote",
                UserName = "bfoote",
                Password = GetHash("maple")
            });

            modelBuilder.Entity<tblUser>().HasData(new tblUser
            {
                Id = userId[3],
                FirstName = "Other",
                LastName = "Other",
                UserName = "sophie",
                Password = GetHash("sophie")
            });
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
