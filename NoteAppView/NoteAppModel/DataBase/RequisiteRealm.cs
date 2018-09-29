using Realms;

namespace NoteAppModel.DataBase
{
    public class RequisiteRealm : RealmObject
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        [PrimaryKey]
        public int RequisiteKey { get; set; }

        /// <summary>
        /// Ключ пользователя
        /// </summary>
        public int UserKey { get; set; }

        /// <summary>
        /// Тип рекизита
        /// </summary>
        public int ReqType { get; set; }

        /// <summary>
        /// Данные
        /// </summary>
        public string Data { get; set; }

        public RequisiteRealm() { }

        public RequisiteRealm(int key, int user, RequisiteTypeEnum type, object data)
        {
            RequisiteKey = key;
            UserKey = user;
            ReqType = (int)type;
            data = Newtonsoft.Json.JsonConvert.SerializeObject(data);
        }
    }
}
