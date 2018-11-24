using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FelinStats_Back.Response
{
    class ServiceTime
    {
        [JsonProperty]
        public int Code { set; get; }

        [JsonProperty]
        public Dictionary<string, Tuple<int, string>> Value { set; get; }
    }
}
