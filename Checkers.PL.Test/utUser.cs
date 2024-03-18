using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.PL.Test
{
    [TestClass]
    public class utUser : utBase<tblUser>
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.IsTrue(base.LoadTest().Count() > 0);
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
