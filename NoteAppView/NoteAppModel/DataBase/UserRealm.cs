using Newtonsoft.Json;
using NoteAppModel.Protocol;
using Realms;
using System;

namespace NoteAppModel.DataBase
{
    public class UserRealm : RealmObject
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        [PrimaryKey]
        public int UserKey { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTimeOffset CreateDate { get; set; }

        /// <summary>
        /// Дата изменения профиля
        /// </summary>
        public DateTimeOffset UpdateDate { get; set; }

        /// <summary>
        /// Флаги
        /// </summary>
        public int Flags { get; set; }

        public UserRealm() { }

        public UserRealm(UserProtocol protocol)
        {
            UserKey = protocol.UserKey;
            Login = protocol.Login;
            Password = protocol.Password;
            CreateDate = protocol.CreateDate;
            UpdateDate = protocol.UpdateDate;
            Flags = protocol.Flags;
        }
    }
}
