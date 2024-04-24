namespace Checkers.BL
{
    public class GameStateManager : GenericManager<tblGameState>
    {
        public GameStateManager(DbContextOptions<CheckersEntities> options) : base(options) { }
        public GameStateManager(ILogger logger, DbContextOptions<CheckersEntities> options) : base(logger, options) { }

        public List<GameState> LoadTest()
        {
            try
            {
                List<GameState> rows = new List<GameState>();
                base.Load()
                    .ForEach(g => rows.Add(
                        new GameState
                        {
                            Id = g.Id,
                            Column = g.Column,
                            IsKing = g.IsKing,
                            Row = g.Row
                        }));

                return rows;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Insert(GameState gameState, bool rollback = false)
        {
            try
            {
                tblGameState row = new tblGameState
                {
                    Column = gameState.Column,
                    IsKing = gameState.IsKing, 
                    Row = gameState.Row
                };
                gameState.Id = row.Id;
                return base.Insert(row, e => e.Row == gameState.Row, rollback);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Guid> InsertAsync(GameState gameState, bool rollback = false)
        {
            try
            {
                tblGameState row = new tblGameState
                {
                    Column = gameState.Column,
                    IsKing = gameState.IsKing,
                    Row = gameState.Row
                };
                Guid id = await InsertAsync(row, e => e.Row == gameState.Row, rollback);
                gameState.Id = id;
                return id;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<GameState>> LoadAsync()
        {

            try
            {
                List<GameState> rows = new List<GameState>();
                (await base.LoadAsync())
                    //.OrderBy(d => d.SortField)
                    .ToList()
                    .ForEach(d => rows.Add(
                        new GameState
                        {
                            Id = d.Id,
                            Column = d.Column,
                            IsKing = d.IsKing,
                            Row = d.Row
                        }));

                return rows;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public GameState LoadById(Guid id)
        {
            try
            {
                tblGameState row = base.LoadById(id);

                if (row != null)
                {
                    GameState gameState = new GameState
                    {
                        Id = row.Id,
                        Column = row.Column,
                        IsKing = row.IsKing,
                        Row = row.Row
                    };

                    return gameState;
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Update(GameState gameState, bool rollback = false)
        {
            try
            {
                int results = base.Update(new tblGameState
                {
                    Column = gameState.Column,
                    IsKing = gameState.IsKing,
                    Row = gameState.Row
                }, rollback);
                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Delete(Guid id, bool rollback = false)
        {
            try
            {
                return base.Delete(id, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
