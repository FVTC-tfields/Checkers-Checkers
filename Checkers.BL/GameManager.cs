using Microsoft.SqlServer.Server;
using System.IO;

namespace Checkers.BL
{
    public class GameManager : GenericManager<tblGame>
    {
        public GameManager(DbContextOptions<CheckersEntities> options) : base(options) { }
        public GameManager(ILogger logger, DbContextOptions<CheckersEntities> options) : base(logger, options) { }

        public List<Game> Load()
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

        public int Insert(Game game, bool rollback = false)
        {
            try
            {
                tblGame row = new tblGame { 
                    GameStateId = game.GameStateId,
                    Name = game.Name,
                    Winner = game.Winner,
                    GameDate = game.GameDate
                };
                game.Id = row.Id;
                return base.Insert(row, e => e.Name == game.Name, rollback);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Guid> InsertAsync(Game game, bool rollback = false)
        {
            try
            {
                tblGame row = new tblGame {
                    GameStateId = game.GameStateId,
                    Name = game.Name,
                    Winner = game.Winner,
                    GameDate = game.GameDate
                };
                Guid id = await InsertAsync(row, e => e.GameStateId == game.GameStateId, rollback);
                game.Id = id;
                return id;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Game>> LoadAsync()
        {

            try
            {
                List<Game> rows = new List<Game>();
                (await base.LoadAsync())
                    .OrderBy(d => d.SortField)
                    .ToList()
                    .ForEach(d => rows.Add(
                        new Game
                        {
                            Id = d.Id,
                            GameStateId=d.GameStateId,
                            Name = d.Name,
                            Winner = d.Winner,
                            GameDate = d.GameDate
                        }));

                return rows;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public Game LoadById(Guid id)
        {
            try
            {
                tblGame row = base.LoadById(id);

                if (row != null)
                {
                    Game game = new Game
                    {
                        Id = row.Id,
                        GameStateId = row.GameStateId,
                        Name = row.Name,
                        Winner = row.Winner,
                        GameDate = row.GameDate
                    };

                    return game;
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

        public int Update(Game game, bool rollback = false)
        {
            try
            {
                return base.Update(new tblGame
                {
                    Id = game.Id,
                    GameStateId = game.GameStateId,
                    Name = game.Name,
                    Winner = game.Winner,
                    GameDate = game.GameDate
                }, rollback);
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
