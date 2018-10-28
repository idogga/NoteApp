using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace NoteAppView.Controllers
{
    public class ImageController
    {
        public byte[] ImageToByteArray(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Bmp);
                return ms.ToArray();
            }
        }
    }
}
