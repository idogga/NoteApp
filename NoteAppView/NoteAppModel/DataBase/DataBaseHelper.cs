using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;

namespace NoteAppModel.DataBase
{
    public class DataBaseHelper
    {
        private object _obj = new object();
        private Logger _logger;
        private Realm _realm;

        public DataBaseHelper(Logger logger)
        {
            _logger = logger;
            string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\RealmDesktop";
            bool isDatabaseInitialized = Directory.Exists(path);
            if (!isDatabaseInitialized)
            {
                Directory.CreateDirectory(path);
            }

            string file = $"{path}\\default.realm";

            RealmConfiguration config = new RealmConfiguration(file);
            _realm = Realm.GetInstance(config);
        }

        /// <summary>
        /// Все записи пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<NoteRealm> GetAllNotes(int userId)
        {
            lock(_obj)
                return _realm.All<NoteRealm>().Where(x => x.UserId == userId).OrderByDescending(y => y.UpdateDate).ToList();
        }

        /// <summary>
        /// Сохранение 
        /// </summary>
        /// <param name="note"></param>
        public void SaveNote(NoteRealm note)
        {
            lock(_obj)
            {
                var list = _realm.All<NoteRealm>().ToList();
                var oldNote = list.FirstOrDefault(x => x.NoteKey == note.NoteKey);
                if (oldNote == null)
                {
                    if (list.Count == 0)
                        note.NoteKey = 1;
                    else
                        note.NoteKey = _realm.All<NoteRealm>().ToList().Max(x => x.NoteKey) + 1;
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
    }
}
