using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.BL.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public Guid GameStateId { get; set; }
        public string Name { get; set; }
        public string Winner { get; set; }
        public DateTime GameDate { get; set; }
        public List<UserGame> Users { get; set; }
        public List<GameState> gameStates { get; set; }
    }
}
