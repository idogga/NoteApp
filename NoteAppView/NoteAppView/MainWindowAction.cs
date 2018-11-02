using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteAppView
{
    /// <summary>
    /// Комманды главного окна
    /// </summary>
    public enum MainWindowAction
    {
        /// <summary>
        /// Авторизация
        /// </summary>
        Auth,
        /// <summary>
        /// Регистрация
        /// </summary>
        Register,
        /// <summary>
        /// Список заметок
        /// </summary>
        List,
        /// <summary>
        /// Настройки
        /// </summary>
        Settings,
        /// <summary>
        /// Новая заявка
        /// </summary>
        New, 
        /// <summary>
        /// Изменение записи
        /// </summary>
        ChangeNote
    }
}
