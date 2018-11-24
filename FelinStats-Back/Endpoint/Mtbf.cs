using Nancy;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace FelinStats_Back.Endpoint
{
    public class Mtbf : NancyModule
    {
        public Mtbf() : base("/mtbf.json")
        {
            // Time between 2 maintenance
            Get("/", x =>
            {
                if (!File.Exists("mtbf.txt"))
                    return (Response.AsJson(new Response.Error()
                    {
                        Code = 503,
                        Message = "mtbf.txt doesn't exist."
                    })
                    .WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type"));

                string[] lines = File.ReadAllLines("mtbf.txt");
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
                List<int> days = new List<int>();
                foreach (var k in mtbfDic)
                {
                    for (int i = 1; i < k.Value.Count; i++)
                    {
                        days.Add((int)(k.Value[i] - k.Value[i - 1]).TotalDays);
                    }
                }
                Dictionary<string, int> final = new Dictionary<string, int>();
                int[] values = Enumerable.Repeat(0, 15).ToArray();
                int sum = 0;
                foreach (var k in days)
                {
                    sum += k;
                    int val = k / 50;
                    if (val > 14)
                        values[14]++;
                    else
                        values[val]++;
                }
                for (int i = 0, y = 0; i < 700; i += 50, y++)
                    final.Add(i + " - " + (i + 50), values[y]);
                final.Add("700+", values[14]);

                return (Response.AsJson(new Response.Mbtf()
                {
                    Code = 200,
                    Value = final,
                    Mean = sum / days.Count
                })
                .WithHeader("Access-Control-Allow-Origin", "*")
                .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type"));
            });
        }
    }
}
