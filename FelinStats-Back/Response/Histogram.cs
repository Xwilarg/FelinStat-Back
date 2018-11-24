using Newtonsoft.Json;
using System.Collections.Generic;

namespace FelinStats_Back.Response
{
    class Histogram
    {
        [JsonProperty]
        public int Code { set; get; }

        [JsonProperty]
        public Dictionary<int, int> Value { set; get; }
    }
}
