using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.BL.Test
{
    [TestClass]
    public class utGame : utBase
    {
        [TestMethod]
        public void LoadTest()
        {
            List<Game> games = new GameManager(options).Load();
            int expected = 4;

            Assert.AreEqual(expected, games.Count);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            Game game = new GameManager(options).Load().FirstOrDefault(x => x.Name == "World War 42");
            Assert.AreEqual(new GameManager(options).LoadById(game.Id).Id, game.Id);
        }

        [TestMethod]
        public void InsertTest()
        {
            GameStateManager gameState = new GameStateManager(options);

            Game game = new Game
            {
                Name = "The Mostest Awesome Game from Albuquerque!!!",
                GameStateId = gameState.Load().FirstOrDefault().Id,
                Winner = "",
                GameDate = DateTime.Now

            };

            int result = new GameManager(options).Insert(game, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Game game = new GameManager(options).Load().FirstOrDefault();
            game.Name = "Blah blah blah";

            Assert.IsTrue(new GameManager(options).Update(game, true) > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Game game = new GameManager(options).Load().FirstOrDefault(x => x.Name == "World War 42");
            Assert.IsTrue(new GameManager(options).Delete(game.Id, true) > 0);
        }
    }
}
