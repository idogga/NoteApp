using System.Collections.Generic;

namespace NoteAppModel
{
    public class NoteTypes
    {
        public enum NoteTypeEnum
        {
            Work,
            Family, 
            Dinner,
            Weekend,
            MAX
        }
        private Dictionary<int, string> _typeContainer;

        public NoteTypes()
        {
            _typeContainer = new Dictionary<int, string>();
            _typeContainer.Add((int)NoteTypeEnum.Dinner, "Ужин");
            _typeContainer.Add((int)NoteTypeEnum.Family, "Семья");
            _typeContainer.Add((int)NoteTypeEnum.Weekend, "Отдых");
            _typeContainer.Add((int)NoteTypeEnum.Work, "Работа");
        }

        /// <summary>
        /// Получение строки типов записей
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public string GetTypesString(IList<int> types)
        {
            var result = "";
            foreach(var type in types)
            {
                result += GetTypeString(type) + ", ";
            }
            if (string.IsNullOrEmpty(result))
                result = "без тэгов";
            else
                result = result.TrimEnd(new char[] { ',', ' ' });
            return result;
        }

        private string GetTypeString(int type)
        {
            if(type >= (int)NoteTypeEnum.MAX)
                return "неопределенный тип";
            try
            {
                var result = _typeContainer[type];
                return result;
            }
            catch
            {
                return "неопределенный тип";
            }
        }
    }
}
