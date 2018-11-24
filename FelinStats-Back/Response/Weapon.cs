using Newtonsoft.Json;

namespace FelinStats_Back.Response
{
    public class WeaponElement
    {

        [JsonProperty]
        public string Date { set; get; }

        [JsonProperty]
        public string Applicant { set; get; }

        [JsonProperty]
        public string Repairor { set; get; }

        [JsonProperty]
        public string InterventionLevel { set; get; }

        [JsonProperty]
        public string ItType { set; get; }
    }

    public class Weapon
    {
        [JsonProperty]
        public int Code { set; get; }

        [JsonProperty]
        public WeaponElement[] Elems { set; get; }
    }
}
