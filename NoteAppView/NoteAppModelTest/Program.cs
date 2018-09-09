using NoteAppModel;
using NoteAppModel.DataBase;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace NoteAppModelTest
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            var logger = new Logger();
            var _dbHelper = new DataBaseHelper(logger);
            var _httpHelper = new HttpHelper(logger);
            UserRealm user;
            if(NeedRegistration())
            {
                user = Registration(_dbHelper, _httpHelper);
            }
            else
            {
                user = Auth(_dbHelper);
            }
            logger.Write("Авторизация пользователя " + user.Login);
            ShowList(user, _dbHelper);
            AddNotes(user, _dbHelper);
            ShowList(user, _dbHelper);
            Console.ReadKey();
        }

        private static void AddNotes(UserRealm user, DataBaseHelper dbHelper)
        {
            var newNote = new NoteRealm();
            newNote.UserId = user.UserKey;
            Console.WriteLine("Введите название записи : ");
            newNote.Title = Console.ReadLine();
            Console.WriteLine("Введите саму запись : ");
            newNote.ContentText = Console.ReadLine();
            dbHelper.SaveNote(newNote);
            Console.WriteLine("Хотите добавить еще запись ? y/n");
            var result = Console.ReadKey();
            if (result.KeyChar == 'y')
            {
                Console.WriteLine();
                AddNotes(user, dbHelper);
            }
        }

        private static void ShowList(UserRealm user, DataBaseHelper dbHelper)
        {
            Console.WriteLine("Записи пользователя в БД : ");
            var listnotes = dbHelper.GetAllNotes(user.UserKey);
            if (listnotes != null || listnotes.Count == 0)
            {
                foreach (var note in listnotes)
                {
                    Console.WriteLine(note.NoteKey + " | " + note.Title + " | " + note.CreateDate + " | " + note.ContentText);
                }
            }
            else
            {
                Console.WriteLine("Нет записей");
            }
        }

        private static UserRealm Registration(DataBaseHelper _dbHelper, HttpHelper _httpHelper)
        {
            var result = new UserRealm();
            Console.WriteLine("Введите логин : ");
            result.Login = Console.ReadLine();
            if (_httpHelper.IsContain(result.Login))
            {
                Console.WriteLine("Пользователь с таким логином существует. Ппопробуйте что-то новое." + Environment.NewLine);
                return Registration(_dbHelper, _httpHelper);
            }
            Console.WriteLine("Введите пароль : ");
            result.Password = Encoding.UTF8.GetString((new SHA1CryptoServiceProvider()).ComputeHash(Encoding.UTF8.GetBytes(Console.ReadLine()))); 
            _dbHelper.SaveUser(result);
            return result; 
        }

        private static bool NeedRegistration()
        {
            Console.WriteLine("Хотите зарегистрироваться? y/n");
            var answer = Console.ReadLine();
            if (answer.ToLowerInvariant() == "y")
                return true;
            if (answer.ToLowerInvariant() == "n")
                return false;
            Console.WriteLine("Введены некоректные данные. Повторите.");
            return NeedRegistration();
        }

        private static UserRealm Auth(DataBaseHelper _dbHelper)
        {
            Console.WriteLine("Введите логин и через пробел пароль : ");
            var str = Console.ReadLine();
            string[] loginData = str.Split(new char[] { ' ' });
            SHA1 sha = new SHA1CryptoServiceProvider();
            var user = _dbHelper.GetUser(loginData[0], Encoding.UTF8.GetString((new SHA1CryptoServiceProvider()).ComputeHash(Encoding.UTF8.GetBytes(loginData[1]))));
            if (user == null)
            {
                Console.WriteLine("Сбой аутенфикации. Попробуйте снова" + Environment.NewLine);
                return Auth(_dbHelper);
            }
            else
            {
                return user;
            }
        }


    }
}
