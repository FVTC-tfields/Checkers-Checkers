#nullable disable

using Checkers.BL.Models;

namespace Checkers.PL.Entities
{
    public class tblGame : IEntity
    {
        public Guid Id { get; set; }
        public Guid GameStateId { get; set; }
        public string Name { get; set; }
        public string Winner { get; set; }
        public DateTime GameDate { get; set; }
        public virtual tblGameState GameState { get; set; }
        
        public string SortField { get { return Name; } }
        public List<UserGame> Users { get; set; }
        public List<GameState> gameStates { get; set; }

        public virtual ICollection<tblUserGame> tblUserGames { get; set; }
        public virtual ICollection<tblUserGame> tblGameStates { get; set; }
    }
}
