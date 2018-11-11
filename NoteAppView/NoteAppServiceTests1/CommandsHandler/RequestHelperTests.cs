using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteAppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteAppService.Tests
{
    [TestClass()]
    public class RequestHelperTests
    {
        [TestMethod()]
        public void RequestHelperTestMethods()
        {
            try
            {
                var helper = new RequestHelper(null);
                Assert.Fail("Должна быть комманда");
            }
            catch { }
        }

        [TestMethod()]
        public void IsContainsTest()
        {
            {
                var helper = new RequestHelper("asdada");
                Assert.IsFalse(helper.IsContains(), "Метод найден");
            }
            {
                var helper = new RequestHelper("GetUser");
                Assert.IsTrue(helper.IsContains(), "Метод не найден");
            }
        }

        [TestMethod()]
        public void ExecuteTest()
        {
            var helper = new RequestHelper("GetUser");
            Assert.IsNotNull(helper.Execute(null), "Метод не выполнен");
        }
    }
}