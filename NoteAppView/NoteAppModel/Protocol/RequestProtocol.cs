using Newtonsoft.Json;

namespace NoteAppModel.Protocol
{
    public class RequestProtocol
    {
        /// <summary>
        /// Ответ на запрос
        /// </summary>
        [JsonProperty("answer")]
        public AnswerProtocolEnum Answer { get; set; }
    }
}
