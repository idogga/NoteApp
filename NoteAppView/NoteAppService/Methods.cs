using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteAppModel;
using NoteAppModel.DataBase;

namespace NoteAppService
{
    public class Methods
    {
        private Dictionary<string, CommandDelegate> _commands;
        private DataBaseHelper _dbHelper;

        public Methods(Logger logger)
        {
            _dbHelper = new DataBaseHelper(logger);
            _commands =new Dictionary<string, CommandDelegate>();
            _commands.Add("AUTH", Authorize);
            _commands.Add("USERCONTAINS", UserContains);
        }

        public Dictionary<string, CommandDelegate> GetAvailableCommands()
        {
            return _commands;
        }

        private string Serialize(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        private string Authorize(string param)
        {
            var user = new NoteAppModel.DataBase.NoteRealm();
            return Serialize(user);
        }

        private string UserContains(string param)
        {
            var result = _dbHelper.UserContains(param);
            return Serialize(result);
        }
    }
}
