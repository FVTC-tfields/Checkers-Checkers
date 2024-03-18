namespace Checkers.BL
{
    public class GameStateManager : GenericManager<tblGameState>
    {
        public List<GameState> LoadTest()
        {
            try
            {
                List<GameState> rows = new List<GameState>();
                base.Load()
                    .ForEach(gs => rows.Add(
                        new GameState
                        {
                            Id = gs.Id,
                            Row = gs.Row,
                            Column = gs.Column,
                            IsKing = gs.IsKing
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
