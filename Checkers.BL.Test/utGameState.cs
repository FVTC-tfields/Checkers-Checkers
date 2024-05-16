using Checkers.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.BL.Test
{
    [TestClass]
    public class utGameState : utBase
    {
        [TestMethod]
        public void LoadTest()
        {
            List<GameState> gameStates = new GameStateManager(options).Load();
            int expected = 5;

            Assert.AreEqual(expected, gameStates.Count);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            GameState gameState = new GameStateManager(options).Load().FirstOrDefault();
            Assert.AreEqual(new GameStateManager(options).LoadById(gameState.Id).Id, gameState.Id);
        }

        [TestMethod]
        public void InsertTest()
        {
            
            GameState gameState = new GameState
            {
                Row = "8",
                Column = "2",
                IsKing = true

            };

            int result = new GameStateManager(options).Insert(gameState, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            GameState gameState = new GameStateManager(options).Load().FirstOrDefault();
            gameState.IsKing = true;

            Assert.IsTrue(new GameStateManager(options).Update(gameState, true) > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            GameState gameState = new GameStateManager(options).Load().FirstOrDefault();
            Assert.IsTrue(new GameStateManager(options).Delete(gameState.Id, true) > 0);
        }

        //[TestMethod]
        //public void utReportTest()
        //{
        //    var entities = new GameStateManager(options).Load();
        //    string[] columns = { "Row", "Column", "Color", "IsKing" };
        //    var data = GameStateManager.ConvertData<GameState>(entities, columns);
        //    Excel.Export("gameStates.xlsx", data);
        //}
    }
}
