using Microsoft.SqlServer.Server;
using Checkers.BL.Models;
using Checkers.BL;

namespace Checkers.API.Test
{
    [TestClass]
    public class utGame : utBase
    {
        [TestMethod]
        public async Task LoadTestAsync()
        {
            await base.LoadTestAsync<Game>(4);
        }

        [TestMethod]
        public async Task InsertTestAsync()
        {
            Game game = new Game { Name = "World War 42" };
            await base.InsertTestAsync<Game>(game);

        }

        [TestMethod]
        public async Task InsertTestAsyncFail()
        {
            Game game = new Game { Name = "World War 42" };
            await base.InsertTestAsync<Game>(game);

        }

        [TestMethod]
        public async Task DeleteTestAsync()
        {
            await base.DeleteTestAsync1<Game>(new KeyValuePair<string, string>("Name", "World War 42"));
        }

        [TestMethod]
        public async Task LoadByIdTestAsync()
        {
            await base.LoadByIdTestAsync<Game>(new KeyValuePair<string, string>("Name", "World War 42"));
        }

        [TestMethod]
        public async Task UpdateTestAsync()
        {
            //Game game = new Game { 
            //    Name = "World War 43",
            //    Winner = "",
            //    GameDate = DateTime.Now,
            //    GameStateId = 
            //};

            Game game = new GameManager(options).Load().FirstOrDefault();

            game.Name = "World War 43";

            await base.UpdateTestAsync<Game>(new KeyValuePair<string, string>("Name", "World War 42"), game);

        }

    }
}
