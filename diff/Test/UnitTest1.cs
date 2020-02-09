using diff;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void NoChanges()
        {
            var diff = new StringDiff("foo", "foo");
            var result = diff.Compute();

            Assert.AreEqual(3, result.Changes.Count());
            Assert.AreEqual(0, result.Additions);
            Assert.AreEqual(0, result.Deletions);
        }

        [TestMethod]
        public void DiffBanana()
        {
            var diff = new StringDiff("banana", "atana");
            var result = diff.Compute();
            // result.Print();
            Assert.AreEqual(1, result.Additions);
            Assert.AreEqual(2, result.Deletions);
            Assert.AreEqual(7, result.Changes.Count());
        }

        [TestMethod]
        public void DiffCommonPrefix()
        {
            var diff = new StringDiff("abcdfghjqz", "abcdefgijkrxyz");
            var result = diff.Compute();
            result.Print();
            Assert.AreEqual(6, result.Additions);
            Assert.AreEqual(2, result.Deletions);
            Assert.AreEqual(16, result.Changes.Count());
        }

        [TestMethod]
        public void FileDiff()
        {
            var diff = new FileDiff("original.txt", "new.txt");
            var result = diff.Compute();
            result.Print();
        }
    }
}
