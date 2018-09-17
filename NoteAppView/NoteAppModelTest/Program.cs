using NoteAppModel;
using NoteAppModel.DataBase;
using System;
using System.Security.Cryptography;
using System.Text;

namespace NoteAppModelTest
{
    public class Program
    {        
        static void Main(string[] args)
        {
            var _httpHelper = new HttpController();
            UserRealm user;
            if(NeedRegistration())
            {
                user = Registration(_httpHelper);
            }
            else
            {
                user = Auth(_httpHelper);
            }
            Logger.GetInstance().Write("Авторизация пользователя " + user.Login);
            ShowList(user, _httpHelper);
            AddNotes(user, _httpHelper);
            ShowList(user, _httpHelper);
            Console.ReadKey();
        }

        private static void AddNotes(UserRealm user, HttpController httpHelper)
        {
            var newNote = new NoteProtocol();
            newNote.UserId = user.UserKey;
            Console.WriteLine("Введите название записи : ");
            newNote.Title = Console.ReadLine();
            Console.WriteLine("Введите саму запись : ");
            newNote.ContentText = Console.ReadLine();
            Console.WriteLine("Введите идентификаторы тэгов через пробел (1 2 3) : ");
            var tagsString = Console.ReadLine();
            var tagsArray = tagsString.Split(new char[] { ' ' });
            foreach(var tag in tagsArray)
            {
                newNote.TagsLinks.Add(int.Parse(tag));
            }
            if (!httpHelper.SaveNote(newNote))
            {
                Console.WriteLine("Не удалось сохранить запись. Попробуйте снова!");
                AddNotes(user, httpHelper);
            }
            Console.WriteLine("Хотите добавить еще запись ? y/n");
            var result = Console.ReadKey();
            if (result.KeyChar == 'y')
            {
                Console.WriteLine();
                AddNotes(user, httpHelper);
            }
        }

        private static void ShowList(UserRealm user, HttpController httpHelper)
        {
            Console.WriteLine("Записи пользователя в БД : ");
            var listnotes = httpHelper.GetAllNotes(user.UserKey);
            if (listnotes != null || listnotes.Count == 0)
            {
                foreach (var note in listnotes)
                {
                    Console.WriteLine(note.NoteKey + " | " + note.Title + " | " + (new NoteTypes()).GetTypesString(note.TagsLinks) + " | " + note.CreateDate + " | " + note.ContentText);
                }
            }
            else
            {
                Console.WriteLine("Нет записей");
            }
        }

        private static UserRealm Registration(HttpController _httpHelper)
        {
            var result = new UserRealm();
            Console.WriteLine("Введите логин : ");
            result.Login = Console.ReadLine();
            if (_httpHelper.IsContain(result.Login))
            {
                Console.WriteLine("Пользователь с таким логином существует. Попробуйте что-то новое." + Environment.NewLine);
                return Registration(_httpHelper);
            }
            Console.WriteLine("Введите пароль : ");
            result.Password = Encoding.UTF8.GetString((new SHA1CryptoServiceProvider()).ComputeHash(Encoding.UTF8.GetBytes(Console.ReadLine())));
            var answer = _httpHelper.SaveUser(result);
            if (answer == null)
            {
                Console.WriteLine("При сохранении произошла ошибка! Попробуйте еще раз!");
                Registration(_httpHelper);
            }
            return answer; 
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

        private static UserRealm Auth(HttpController _httpHelper)
        {
            Console.WriteLine("Введите логин и через пробел пароль : ");
            var str = Console.ReadLine();
            string[] loginData = str.Split(new char[] { ' ' });
            SHA1 sha = new SHA1CryptoServiceProvider();
            var user = _httpHelper.GetUser(loginData[0], Encoding.UTF8.GetString((new SHA1CryptoServiceProvider()).ComputeHash(Encoding.UTF8.GetBytes(loginData[1]))));
            if (user == null)
            {
                Console.WriteLine("Сбой аутенфикации. Попробуйте снова" + Environment.NewLine);
                return Auth(_httpHelper);
            }
            else
            {
                return user;
            }
        }

    }
}
