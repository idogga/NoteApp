using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NoteAppService.Tests
{
    [TestClass()]
    public class MethodsTests
    {
        /// <summary>
        /// проверка на получение словаря с доступными коммандами
        /// </summary>
        [TestMethod()]
        public void GetAvailableCommandsTest()
        {
            var actualmethods = new Methods();
            foreach(var actual in actualmethods.GetAvailableCommands())
            {
                Assert.IsNotNull(actual.Key, "Название должно быть");
                Assert.IsNotNull(actual.Value, "Метод должен быть");
            }
        }
    }
}