using NoteAppModel.Protocol;

namespace NoteAppView
{
    public static class UserExtension
    {
        /// <summary>
        /// Удаление информации
        /// </summary>
        public static void Remove(this UserProtocol user)
        {
            ViewDataController.GetInstance().FileController.RemoveFile("user.tusur");
            ViewDataController.GetInstance().UserData = null;
        }
    }
}
