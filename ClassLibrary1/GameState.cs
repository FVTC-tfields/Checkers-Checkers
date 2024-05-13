using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.BL.Models
{
    public class GameState
    {
        public Guid Id { get; set; }
        public string Row { get; set; }
        public string Column { get; set; }
        public bool IsKing { get; set; }

    }
}
