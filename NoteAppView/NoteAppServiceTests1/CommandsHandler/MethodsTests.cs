using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NoteAppService.Tests
{
    [TestClass()]
    public class MethodsTests
    {
        [TestMethod()]
        public void GetAvailableCommandsTest()
        {
            var methods = new Methods();
            foreach(var meth in methods.GetAvailableCommands())
            {
                Assert.IsNotNull(meth.Key, "Название должно быть");
                Assert.IsNotNull(meth.Value, "Метод должен быть");
            }
        }
    }
}