using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.PL.Test
{
    [TestClass]
    public class utGame : utBase<tblGame>
    {
        [TestMethod]
        public void LoadTest()
        {
            int expected = 4;
            var games = base.LoadTest();
            Assert.AreEqual(expected, games.Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            
            int rowsAffected = InsertTest(new tblGame
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                GameDate = DateTime.Now,
                GameStateId = base.LoadTest().FirstOrDefault().GameStateId,
                Winner = ""
            });
            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblGame row = base.LoadTest().FirstOrDefault();

            if (row != null)
            {
                row.Name = "Hello there";
                int rowsAffected = UpdateTest(row);
                Assert.AreEqual(1, rowsAffected);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblGame row = dc.tblGames.FirstOrDefault(x => x.Name == "World War 42");

            if (row != null)
            {
                int rowsAffected = DeleteTest(row);

                Assert.IsTrue(rowsAffected == 1);
            }
        }
    }
}
