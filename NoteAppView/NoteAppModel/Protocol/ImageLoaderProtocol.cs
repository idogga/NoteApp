using Newtonsoft.Json;
using NoteAppModel.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteAppModel.Protocol
{
    public class ImageLoaderProtocol: RequestProtocol
    {
        [JsonProperty("key")]
        public int ImageKey { get; set; }

        /// <summary>
        /// Картинка
        /// </summary>
        [JsonProperty("data")]
        public byte[] ImageSource { get; set; }

        public ImageLoaderProtocol() { }

        public ImageLoaderProtocol(ImageRealm realm)
        {
            ImageKey = realm.ImageKey;
            ImageSource = realm.ImageSource;
        }
    }
}
