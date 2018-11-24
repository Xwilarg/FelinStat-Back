using Nancy;
using System.Collections.Generic;
using System.IO;

namespace FelinStats_Back.Endpoint
{
    public class Mtbf : NancyModule
    {
        public Mtbf() : base("/mtbf.json")
        {
            // Time between 2 failures
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
                Dictionary<string, int> mtbfDic = new Dictionary<string, int>();
                foreach (string s in lines)
                {
                    string[] datas = s.Split(';');
                    mtbfDic.Add(datas[0], int.Parse(datas[1]));
                }
                return (Response.AsJson(new Response.Histogram()
                {
                    Code = 200,
                    Value = mtbfDic
                })
                .WithHeader("Access-Control-Allow-Origin", "*")
                .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type"));
            });
        }
    }
}
