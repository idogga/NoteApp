using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NoteAppModel.Tests
{
    [TestClass()]
    public class NoteTypesTests
    {
        /// <summary>
        /// Проверка получения типов записи
        /// </summary>
        /// <remarks>
        /// позитивный тест (1,2)
        /// </remarks>
        [TestMethod()]
        public void GetTypesStringTest()
        {
            var type = new NoteTypes();

            var str = type.GetTypesString(new List<int>() { 1, 2 });
            Assert.IsFalse(string.IsNullOrEmpty(str), "Строка пустая");

        }

        /// <summary>
        /// Проверка получения типов записи
        /// </summary>
        /// <remarks>
        /// негативный тест -1, int.MaxValue 
        /// </remarks>
        [TestMethod()]
        public void GetTypesStringTestNegarive()
        {
            var type = new NoteTypes();

            var str = type.GetTypesString(new List<int>() { -1, int.MaxValue });
            Assert.IsFalse(string.IsNullOrEmpty(str), "Строка пустая");

        }

        /// <summary>
        /// Проверка получения типов записи
        /// </summary>
        /// <remarks>
        /// позитивный тест (никакие типы не выбраны)
        /// </remarks>
        [TestMethod()]
        public void GetTypesStringTestNull()
        {
            var type = new NoteTypes();
            var str = type.GetTypesString(new List<int>());
            Assert.IsFalse(string.IsNullOrEmpty(str), "Строка пустая");
        }
    }
}