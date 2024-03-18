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
            int expected = 0;
            var usergames = base.LoadTest();
            Assert.AreEqual(expected, usergames.Count);
        }

        [TestMethod]
        public void InsertTest()
        {

        }

        [TestMethod]
        public void UpdateTest()
        {

        }

        [TestMethod]
        public void DeleteTest()
        {

        }
    }
}
