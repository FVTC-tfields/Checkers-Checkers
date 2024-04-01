#nullable disable
namespace Checkers.PL.Entities
{
    public class tblUserGame : IEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid GameId { get; set; }

        public string Color { get; set; }

        public virtual tblUser User { get; set; }

        public virtual tblGame Game { get; set; }

        public virtual ICollection<tblGame> tblGames { get; set; }

        public virtual ICollection<tblUser> tblUsers { get; set; }

    }
}
