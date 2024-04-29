using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.PL.Test
{
    [TestClass]
    public class utGameState : utBase<tblGameState>
    {
        [TestMethod]
        public void LoadTest()
        {
            int expected = 5;
            var gamestates = base.LoadTest();
            Assert.AreEqual(expected, gamestates.Count);
        }

        [TestMethod]
        public void InsertTest()
        {

            int rowsAffected = InsertTest(new tblGameState
            {
                Id = Guid.NewGuid(),
                IsKing = true,
                Row = "1",
                Column = "2"
            });
            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblGameState row = base.LoadTest().FirstOrDefault();

            if (row != null)
            {
                row.IsKing = true;
                int rowsAffected = UpdateTest(row);
                Assert.AreEqual(1, rowsAffected);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblGameState row = dc.tblGameStates.FirstOrDefault();

            if (row != null)
            {
                int rowsAffected = DeleteTest(row);

                Assert.IsTrue(rowsAffected == 1);
            }
        }
    }
}
