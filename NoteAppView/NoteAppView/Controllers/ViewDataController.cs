using NoteAppModel.Protocol;

namespace NoteAppView
{
    public class ViewDataController
    {
        #region private
        private static ViewDataController _instance;

        #endregion

        private ViewDataController() { }

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
    }
}
