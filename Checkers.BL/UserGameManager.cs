namespace Checkers.BL
{
    public class UserGameManager : GenericManager<tblUserGame>
    {
        

        public UserGameManager(DbContextOptions<CheckersEntities> options) : base(options) { }
        public UserGameManager(ILogger logger, DbContextOptions<CheckersEntities> options) : base(logger, options) { }

        public List<UserGame> LoadTest()
        {
            try
            {
                List<UserGame> rows = new List<UserGame>();
                base.Load()
                    .ForEach(ug => rows.Add(
                        new UserGame
                        {
                            Id = ug.Id,
                            UserId = ug.UserId,
                            GameId = ug.GameId,
                            Color = ug.Color
                        }));

                return rows;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Insert(UserGame userGame, bool rollback = false)
        {
            try
            {
                tblUserGame row = new tblUserGame
                {
                    UserId = userGame.UserId,
                    GameId = userGame.GameId,
                    Color = userGame.Color
                };
                userGame.Id = row.Id;
                return base.Insert(row, e => e.UserId == userGame.UserId, rollback);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Guid> InsertAsync(UserGame userGame, bool rollback = false)
        {
            try
            {
                tblUserGame row = new tblUserGame
                {
                    UserId = userGame.UserId,
                    GameId = userGame.GameId,
                    Color = userGame.Color
                };
                Guid id = await InsertAsync(row, e => e.UserId == userGame.UserId, rollback);
                userGame.Id = id;
                return id;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<UserGame>> LoadAsync()
        {

            try
            {
                List<UserGame> rows = new List<UserGame>();
                (await base.LoadAsync())
                    //.OrderBy(d => d.SortField)
                    .ToList()
                    .ForEach(d => rows.Add(
                        new UserGame
                        {
                            Id = d.Id,
                            UserId = d.UserId,
                            GameId = d.GameId,
                            Color = d.Color
                        }));

                return rows;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public UserGame LoadById(Guid id)
        {
            try
            {
                tblUserGame row = base.LoadById(id);

                if (row != null)
                {
                    UserGame userGame = new UserGame
                    {
                        Id = row.Id,
                        UserId = row.UserId,
                        GameId = row.GameId,
                        Color = row.Color
                    };

                    return userGame;
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

        public int Update(UserGame userGame, bool rollback = false)
        {
            try
            {
                int results = base.Update(new tblUserGame
                {
                    UserId = userGame.UserId,
                    GameId = userGame.GameId,
                    Color = userGame.Color
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
