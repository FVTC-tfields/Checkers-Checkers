using System.IO;

namespace Checkers.BL
{
    public class GameManager : GenericManager<tblGame>
    {
        public List<Game> LoadTest()
        {
            try
            {
                List<Game> rows = new List<Game>();
                base.Load()
                    .ForEach(g => rows.Add(
                        new Game
                        {
                            Id = g.Id,
                            GameStateId = g.GameStateId,
                            Name = g.Name,
                            Winner = g.Winner,
                            GameDate = g.GameDate
                        }));

                return rows;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
