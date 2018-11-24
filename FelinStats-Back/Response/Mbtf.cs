using Newtonsoft.Json;
using System.Collections.Generic;

namespace FelinStats_Back.Response
{
    class Mbtf
    {
        [JsonProperty]
        public int Code { set; get; }

        [JsonProperty]
        public Dictionary<string, int> Value { set; get; }

        [JsonProperty]
        public int Mean { set; get; }
    }
}
