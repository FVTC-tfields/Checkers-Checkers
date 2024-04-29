using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.PL.Test
{
    [TestClass]
    public class utUserGame : utBase<tblUserGame>
    {
        [TestMethod]
        public void LoadTest()
        {
            int expected = 3;
            var usergames = base.LoadTest();
            Assert.AreEqual(expected, usergames.Count);
        }

        [TestMethod]
        public void InsertTest()
        {

            int rowsAffected = InsertTest(new tblUserGame
            {
                Id = Guid.NewGuid(),
                Color = "Black",
                UserId = base.LoadTest().FirstOrDefault().UserId,
                GameId = base.LoadTest().FirstOrDefault().GameId
            }) ;
            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblUserGame row = base.LoadTest().FirstOrDefault();

            if (row != null)
            {
                row.Color = "Red";
                int rowsAffected = UpdateTest(row);
                Assert.AreEqual(1, rowsAffected);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblUserGame row = dc.tblUserGames.FirstOrDefault();

            if (row != null)
            {
                int rowsAffected = DeleteTest(row);

                Assert.IsTrue(rowsAffected == 1);
            }
        }
    }
}
