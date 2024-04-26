#nullable disable

namespace Checkers.PL.Entities
{
    public class tblGameState : IEntity
    {
        public Guid Id { get; set; }
        public string Row { get; set; }
        public string Column { get; set; }
        public bool IsKing { get; set; }
        public virtual ICollection<tblGame> tblGames { get; set; }
    }
}
