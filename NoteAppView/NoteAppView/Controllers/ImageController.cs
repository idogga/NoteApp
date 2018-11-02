﻿using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace NoteAppView.Controllers
{
    /// <summary>
    /// Контроллер управления картинками
    /// </summary>
    public class ImageController
    {
        /// <summary>
        /// Преобразование картинки в массив байтов
        /// </summary>
        /// <param name="bitmapImage">Картинка</param>
        /// <returns>Массив байтов картинки</returns>
        public byte[] ImageToByteArray(BitmapImage bitmapImage)
        {
            var bitmap = BitmapImage2Bitmap(bitmapImage);
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Bmp);
                return ms.ToArray();
            }
        }

        private Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }
    }
}