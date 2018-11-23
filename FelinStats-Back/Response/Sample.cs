using Newtonsoft.Json;

namespace FelinStats_Back.Response
{
    public class Sample
    {
        [JsonProperty]
        public int Code { set; get; }

        [JsonProperty]
        public string Message { set; get; }
    }
}
