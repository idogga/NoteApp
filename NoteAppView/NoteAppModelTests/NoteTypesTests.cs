using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteAppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteAppModel.Tests
{
    [TestClass()]
    public class NoteTypesTests
    {
        [TestMethod()]
        public void GetTypesStringTest()
        {
            var type = new NoteTypes();
            {
                var str = type.GetTypesString(new List<int>() { 1, 2 });
                Assert.IsFalse(string.IsNullOrEmpty(str), "Строка пустая");
            }
            {
                var str = type.GetTypesString(new List<int>() { -1, int.MaxValue });
                Assert.IsFalse(string.IsNullOrEmpty(str), "Строка пустая");
            }
        }

        [TestMethod()]
        public void GetTypesStringTestNull()
        {
            var type = new NoteTypes();
            var str = type.GetTypesString(new List<int>());
            Assert.IsFalse(string.IsNullOrEmpty(str), "Строка пустая");
        }
    }
}