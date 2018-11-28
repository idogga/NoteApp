using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NoteAppService.Tests
{
    [TestClass()]
    public class RequestHelperTests
    {
        /// <summary>
        /// создание экземпляра RequestHelper
        /// </summary>
        /// <remarks>
        /// негативный тест (null)
        /// </remarks>
        [TestMethod()]
        public void RequestHelperTestConstructor()
        {
            try
            {
                var helper = new RequestHelper(null);
                Assert.Fail("Должна быть комманда");
            }
            catch { }
        }

        /// <summary>
        /// создание экземпляра RequestHelper
        /// </summary>
        /// <remarks>
        /// негативный тест (рандомная строка)
        /// </remarks>
        [TestMethod()]
        public void IsContainsTestNegative()
        {
            // Какая то рандомная строка, главное что бы не совпало с реальной командой
                var helper = new RequestHelper("asdada");
                Assert.IsFalse(helper.IsContains(), "Метод найден");
            
        }

        /// <summary>
        /// создание экземпляра RequestHelper
        /// </summary>
        /// <remarks>
        /// позитивный тест (GetUser)
        /// </remarks>
        [TestMethod()]
        public void IsContainsTestPossitive()
        {
            var helper = new RequestHelper("GetUser");
            Assert.IsTrue(helper.IsContains(), "Метод не найден");
        }

        /// <summary>
        /// Проверка на выполнение методда
        /// </summary>
        /// <remarks>
        /// позитивный тест (GetUser)
        /// </remarks>
        [TestMethod()]
        public void ExecuteTest()
        {
            var helper = new RequestHelper("GetUser");
            //  выполнение getuser с входными параметрами null
            Assert.IsNotNull(helper.Execute(null), "Метод не выполнен");
        }
    }
}