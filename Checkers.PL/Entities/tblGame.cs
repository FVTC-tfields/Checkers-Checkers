using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public virtual ICollection<tblUserGame> tblUserGames { get; set; }

    }
}
