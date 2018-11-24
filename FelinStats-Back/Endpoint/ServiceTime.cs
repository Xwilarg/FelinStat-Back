using Nancy;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace FelinStats_Back.Endpoint
{
    public class ServiceTime : NancyModule
    {
        public ServiceTime() : base("/serviceTime.json")
        {
            // Time between 2 failures
            Get("/", x =>
            {
                if (!File.Exists("serviceTime.txt"))
                    return (Response.AsJson(new Response.Error()
                    {
                        Code = 503,
                        Message = "serviceTime.txt doesn't exist."
                    })
                    .WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type"));

                string[] lines = File.ReadAllLines("serviceTime.txt");
                SortedDictionary<string, List<DateTime>> mtbfDic = new SortedDictionary<string, List<DateTime>>();
                foreach (string s in lines)
                {
                    string[] datas = s.Split(';');
                    if (datas[0] == "")
                        continue;
                    DateTime dt = DateTime.ParseExact(datas[1], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (!mtbfDic.ContainsKey(datas[0]))
                    {
                        var time = new List<DateTime>();
                        time.Add(dt);
                        mtbfDic.Add(datas[0], time);
                    }
                    else
                        mtbfDic[datas[0]].Add(dt);
                }
                Dictionary<string, int> final = new Dictionary<string, int>();
                foreach (var k in mtbfDic)
                    final.Add(k.Key, (int)(DateTime.Now - k.Value[k.Value.Count - 1]).TotalDays);
                final.ToList().Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
                Dictionary<string, int> sortedList = new Dictionary<string, int>();
                foreach (var k in final.OrderByDescending(x2 => x2.Value))
                    sortedList.Add(k.Key, k.Value);
                return (Response.AsJson(new Response.Histogram()
                {
                    Code = 200,
                    Value = sortedList
                })
                .WithHeader("Access-Control-Allow-Origin", "*")
                .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type"));
            });
        }
    }
}
