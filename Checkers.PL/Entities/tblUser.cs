using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.PL.Entities
{
    public class tblUser : IEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public virtual ICollection<tblUserGame> tblUserGames { get; set; }
        public string SortField { get { return LastName; } }

    }
}
