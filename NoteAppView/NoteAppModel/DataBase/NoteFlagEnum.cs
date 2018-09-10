using System;

namespace NoteAppModel.DataBase
{
    [Flags]
    public enum NoteFlagEnum
    {
        /// <summary>
        /// Нет флагов
        /// </summary>
        None = 0,

        /// <summary>
        /// Удалена
        /// </summary>
        Delete = 1<<0,

        /// <summary>
        /// Любимая
        /// </summary>
        IsFavourite = 1<<1
    }
}
