using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NoteAppModel.DataBase.Tests
{
    [TestClass()]
    public class DataBaseHelperTests
    {
        private DataBaseHelper dbHelper;

        public void SetUp()
        {
            dbHelper = new DataBaseHelper(true);
        }


        /// <summary>
        /// Тестирование получения всех данных от пользователя
        /// </summary>
        /// <remarks>
        /// негативный тест (-1)
        /// </remarks>
        [TestMethod()]
        public void GetAllNotesTest()
        {
            var answer = dbHelper.GetAllNotes(-1);
            Assert.IsNull(answer, "Нашлись данные");
        }

        /// <summary>
        /// Сохранение заметки
        /// </summary>
        /// <remarks>
        /// позитивный тест (пустая заявка)
        /// </remarks>
        [TestMethod()]
        public void SaveNoteTest()
        {
            try
            {
                dbHelper.SaveNote(new NoteRealm());
            }
            catch
            {
                Assert.Fail("asd");
            }
        }

        /// <summary>
        /// Сохранение заметки
        /// </summary>
        /// <remarks>
        /// негативный тест (null)
        /// </remarks>
        [TestMethod()]
        public void SaveNoteTestNull()
        {
            try
            {
                dbHelper.SaveNote(null);
            }
            catch
            {
                Assert.Fail("asd");
            }
        }

        /// <summary>
        /// Поиск пользователя
        /// </summary>
        /// <remarks>
        /// позитивный тест (login = "werty", pass ="dfghj")
        /// </remarks>
        [TestMethod()]
        public void GetUserTest()
        {
            //  в качестве полей выбраны случайные строки
            Assert.IsNull(dbHelper.GetUser("werty", "dfghj"));
        }

        /// <summary>
        /// Имеется ли пользователь с таким логином в базе данных
        /// </summary>
        /// <remarks>
        /// позитивный тест (login = "asdfghjkl")
        /// </remarks>
        [TestMethod()]
        public void UserContainsTest()
        {
            //  в качестве полей выбраны случайная строка
            Assert.IsFalse(dbHelper.UserContains("asdfghjkl"));
        }

        /// <summary>
        /// Сохранение пользователя в БД
        /// </summary>
        /// <remarks>
        /// позитивный тест (пустой пользователь)
        /// </remarks>
        [TestMethod()]
        public void SaveUserTest()
        {
            Assert.IsNotNull(dbHelper.SaveUser(new UserRealm()));
        }

        /// <summary>
        /// Сохранение пользователя в БД
        /// </summary>
        /// <remarks>
        /// негативный тест (null)
        /// </remarks>
        [TestMethod()]
        public void SaveUserTestNull()
        {
            var dbHelper = new DataBaseHelper(true);
            Assert.IsNotNull(dbHelper.SaveUser(null));
        }

        /// <summary>
        /// сохранение картинки в БД
        /// </summary>
        /// <remarks>
        /// позитивный тест (рандомная запись с ключом 1)
        /// </remarks>
        [TestMethod()]
        public void SaveImageTest()
        {
            Assert.IsTrue(0!=dbHelper.SaveImage(new ImageRealm() { ImageKey = 1, ImageSource = new byte[1024] }));
        }

        /// <summary>
        /// сохранение картинки в БД
        /// </summary>
        /// <remarks>
        /// негативный тест (null)
        /// </remarks>
        [TestMethod()]
        public void SaveImageTestNull()
        {
            Assert.IsTrue(0 != dbHelper.SaveImage(null));
        }

        /// <summary>
        /// поиск картинки в базе данных
        /// </summary>
        /// <remarks>
        /// негативный тест (ключ картинки = -1)
        /// </remarks>
        [TestMethod()]
        public void GetImageTest()
        {
            Assert.IsNull(dbHelper.GetImage(-1));
        }
    }
}