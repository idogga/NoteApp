using NoteAppModel.Protocol;
using NoteAppView.Controllers;

namespace NoteAppView
{
    public class ViewDataController
    {
        #region private
        private static ViewDataController _instance;
        #endregion

        private ViewDataController()
        {
            FileController = new FileController();
        }

        public static ViewDataController GetInstance()
        {
            if (_instance == null)
                _instance = new ViewDataController();
            return _instance;
        }

        /// <summary>
        /// Данные о пользователе
        /// </summary>
        public UserProtocol UserData { get; set; }

        /// <summary>
        /// Управление фалами
        /// </summary>
        public FileController FileController { get; set; }

        /// <summary>
        /// Управление картинками
        /// </summary>
        public ImageController ImageController { get; set; }
    }
}
