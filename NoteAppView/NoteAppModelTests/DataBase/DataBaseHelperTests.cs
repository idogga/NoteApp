using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace NoteAppModel.DataBase.Tests
{
    [TestClass()]
    public class DataBaseHelperTests
    {
        [TestMethod()]
        public void GetAllNotesTest()
        {
            var dbHelper = new DataBaseHelper(true);
            {
                var answer = dbHelper.GetAllNotes(-1);
                Assert.IsNull(answer, "Нашлись данные");
            }
        }

        [TestMethod()]
        public void SaveNoteTest()
        {
            var dbHelper = new DataBaseHelper(true);
            try
            {
                dbHelper.SaveNote(new NoteRealm());
            }
            catch
            {
                Assert.Fail("asd");
            }
        }

        [TestMethod()]
        public void SaveNoteTestNull()
        {
            var dbHelper = new DataBaseHelper(true);
            try
            {
                dbHelper.SaveNote(null);
            }
            catch
            {
                Assert.Fail("asd");
            }
        }

        [TestMethod()]
        public void GetUserTest()
        {
            var dbHelper = new DataBaseHelper(true);
            Assert.IsNull(dbHelper.GetUser("werty", "dfghj"));
        }

        [TestMethod()]
        public void UserContainsTest()
        {
            var dbHelper = new DataBaseHelper(true);
            Assert.IsFalse(dbHelper.UserContains("asdfghjkl"));
        }

        [TestMethod()]
        public void SaveUserTest()
        {
            var dbHelper = new DataBaseHelper(true);
            Assert.IsNotNull(dbHelper.SaveUser(new UserRealm()));
        }

        [TestMethod()]
        public void SaveUserTestNull()
        {
            var dbHelper = new DataBaseHelper(true);
            Assert.IsNotNull(dbHelper.SaveUser(null));
        }

        [TestMethod()]
        public void SaveImageTest()
        {
            var dbHelper = new DataBaseHelper(true);
            Assert.IsTrue(0!=dbHelper.SaveImage(new ImageRealm() { ImageKey = 1, ImageSource = new byte[1024] }));
        }

        [TestMethod()]
        public void SaveImageTestNull()
        {
            var dbHelper = new DataBaseHelper(true);
            Assert.IsTrue(0 != dbHelper.SaveImage(null));
        }

        [TestMethod()]
        public void GetImageTest()
        {
            var dbHelper = new DataBaseHelper(true);
            Assert.IsNull(dbHelper.GetImage(-1));
        }
    }
}