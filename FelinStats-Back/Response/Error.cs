﻿using Newtonsoft.Json;

namespace FelinStats_Back.Response
{
    public class Error
    {
        [JsonProperty]
        public int Code { set; get; }

        [JsonProperty]
        public string Message { set; get; }
    }
}
