using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.BL.Test
{
    [TestClass]
    public class utUserGame : utBase
    {
        [TestMethod]
        public void LoadTest()
        {
            List<UserGame> userGames = new UserGameManager(options).Load();
            int expected = 3;

            Assert.AreEqual(expected, userGames.Count);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            UserGame userGame = new UserGameManager(options).Load().FirstOrDefault();
            Assert.AreEqual(new UserGameManager(options).LoadById(userGame.Id).Id, userGame.Id);
        }

        [TestMethod]
        public void InsertTest()
        {
            Game game = new GameManager(options).Load().FirstOrDefault();
            User user = new UserManager(options).Load().FirstOrDefault(x => x.FirstName == "Other");

            UserGame userGame = new UserGame
            {
                Color = "Red",
                GameId = game.Id,
                UserId = user.Id
            };

            int result = new UserGameManager(options).Insert(userGame, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            UserGame userGame = new UserGameManager(options).Load().FirstOrDefault();
            userGame.Color = "Red";

            Assert.IsTrue(new UserGameManager(options).Update(userGame, true) > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            UserGame userGame = new UserGameManager(options).Load().FirstOrDefault();
            Assert.IsTrue(new UserGameManager(options).Delete(userGame.Id, true) > 0);
        }
    }
}
