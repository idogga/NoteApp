using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NoteAppModel.Tests
{
    [TestClass()]
    public class LoggerTests
    {
        /// <summary>
        /// запись строки в файл логов
        /// </summary>
        /// <remarks>
        /// позитивный тест (test)
        /// </remarks>
        [TestMethod()]
        public void WriteTestString()
        {
            Logger.GetInstance().Write("test");
            var actualStrings = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "Log-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
            foreach (var actual
                in actualStrings)
                Assert.IsFalse(string.IsNullOrEmpty(actual), "лог должен быть");
        }

        /// <summary>
        /// запись объекта в файл логов
        /// </summary>
        /// <remarks>
        /// позитивный тест (test)
        /// </remarks>
        [TestMethod()]
        public void WriteTestObject()
        {
            // рандомный объект 
            Logger.GetInstance().Write(new { field1 = 0, field2 = "asdfghj" });
            var actualStrings = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "Log-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
            foreach (var actual in actualStrings)
                Assert.IsFalse(string.IsNullOrEmpty(actual), "лог должен быть");
        }

        /// <summary>
        /// запись ошибки в файл логов
        /// </summary>
        /// <remarks>
        /// позитивный тест (какая сгенерированая ошибка)
        /// </remarks>
        [TestMethod()]
        public void WriteTestException()
        {
            Logger.GetInstance().Write(new Exception("Тестовая ошибка"));
            var actualStrings = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "Errors-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
            foreach (var actual in actualStrings)
                Assert.IsFalse(string.IsNullOrEmpty(actual), "лог должен быть");
        }

        /// <summary>
        /// запись строк в файл логов в режиме мультипоточности
        /// </summary>
        /// <remarks>
        /// позитивный тест (какая сгенерированая ошибка)
        /// </remarks>
        [TestMethod()]
        public void WriteTestMultiThread()
        {
            var tasks = new List<Task>();
            for(int i = 0; i<20;i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                Logger.GetInstance().Write("test")));
            }
            Task.WaitAll(tasks.ToArray());
            var actualStrings = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "Log-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
            foreach (var actual in actualStrings)
                Assert.IsFalse(string.IsNullOrEmpty(actual), "лог должен быть");
        }

        [TestCleanup()]
        public void Clean()
        {
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Log-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
        }
    }
}