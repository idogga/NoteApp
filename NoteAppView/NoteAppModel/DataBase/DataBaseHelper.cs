using Realms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace NoteAppModel.DataBase
{
    public class DataBaseHelper
    {
        private object _obj = new object();
        private Realm _realm;

        public DataBaseHelper()
        {
            string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\RealmDesktop";
            bool isDatabaseInitialized = Directory.Exists(path);
            if (!isDatabaseInitialized)
            {
                Directory.CreateDirectory(path);
            }

            string file = $"{path}\\noteBase.realm";

            RealmConfiguration config = new RealmConfiguration(file)
            {
                SchemaVersion = 0
            };
            _realm = Realm.GetInstance(config);
        }

        /// <summary>
        /// Все записи пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<NoteRealm> GetAllNotes(int userId)
        {
            lock (_obj)
            {
                var list = _realm.All<NoteRealm>().Where(x => x.UserId == userId).OrderByDescending(y => y.UpdateDate).ToList();
                return list.Where(x => !x.IsFlagOpen(NoteFlagEnum.Delete)).ToList();
            }
        }

        /// <summary>
        /// Сохранение 
        /// </summary>
        /// <param name="note"></param>
        public void SaveNote(NoteRealm note)
        {
            lock (_obj)
            {
                var list = _realm.All<NoteRealm>().ToList();
                var oldNote = list.FirstOrDefault(x => x.NoteKey == note.NoteKey);
                if (oldNote == null)
                {
                    if (list.Count == 0)
                        note.NoteKey = 1;
                    else
                        note.NoteKey = list.Max(x => x.NoteKey) + 1;
                    note.CreateDate = DateTimeOffset.Now;
                }
                else
                {
                    note.NoteKey = oldNote.NoteKey;
                    note.CreateDate = oldNote.CreateDate;
                }
                note.UpdateDate = DateTimeOffset.Now;
                _realm.Write(() => _realm.Add(note, update: true));
            }

        }

        /// <summary>
        /// Поиск пользователя
        /// </summary>
        /// <param name="login">логин</param>
        /// <param name="password">пароль</param>
        /// <returns>данные пользователя</returns>
        public UserRealm GetUser(string login, string password)
        {
            lock (_obj)
            {
                var list = _realm.All<UserRealm>().ToList();
                var user = list.FirstOrDefault(x => x.Login == login & x.Password == password);
                return user;
            }
        }

        /// <summary>
        /// Имеется ли пользователь с таким логином в базе
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool UserContains(string login)
        {
            lock (_obj)
            {
                var user = _realm.All<UserRealm>().FirstOrDefault(x => x.Login == login);
                return user != null;
            }
        }
    
        /// <summary>
        /// Сохранение пользователя
        /// </summary>
        /// <param name="user"></param>
        public UserRealm SaveUser(UserRealm user)
        {
            lock (_obj)
            {
                var list = _realm.All<UserRealm>().ToList();
                var oldUser = list.FirstOrDefault(x => x.UserKey == user.UserKey);
                if (oldUser == null)
                {
                    if (list.Count == 0)
                        user.UserKey = 1;
                    else
                        user.UserKey = list.Max(x => x.UserKey) + 1;
                    user.CreateDate = DateTimeOffset.Now; 
                }
                else
                {
                    user.UserKey = oldUser.UserKey;
                    user.CreateDate = oldUser.CreateDate;
                }
                user.UpdateDate = DateTimeOffset.Now;
                _realm.Write(() => _realm.Add(user, update: true));
                return user;
            }
        }

        /// <summary>
        /// Сохранение картинки
        /// </summary>
        /// <param name="imageRealm"></param>
        public int SaveImage(ImageRealm imageRealm)
        {
            lock(_obj)
            {
                var list = _realm.All<ImageRealm>().ToList();
                var imageMD = GetHashFromByteArray(imageRealm.ImageSource);
                var currentNumber = 0;
                foreach(var image in list)
                {
                    if (imageMD.Equals(GetHashFromByteArray(image.ImageSource)))
                    {
                        return currentNumber;
                    }
                }
                imageRealm.ImageKey = list.Max(x => x.ImageKey) + 1;
                _realm.Write(() => _realm.Add(imageRealm, update: true));
                return imageRealm.ImageKey;
            }
        }
        

       private string GetHashFromByteArray(byte[] array)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] byteHashed = md5.ComputeHash(array);
            }
            StringBuilder result = new StringBuilder(array.Length * 2);
            for (int i = 0; i < array.Length; i++)
                result.Append(array[i].ToString("x2"));
            return result.ToString();
        }
    }
}
