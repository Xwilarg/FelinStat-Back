using Nancy;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace FelinStats_Back.Endpoint
{
    public class Mttr : NancyModule
    {
        public Mttr() : base("/mttr.json")
        {
            Get("/", x =>
            {
                if (!File.Exists("mttr.txt"))
                    return (Response.AsJson(new Response.Error()
                    {
                        Code = 503,
                        Message = "mttr.txt doesn't exist."
                    }));
                string[] lines = File.ReadAllLines("mttr.txt");
                Dictionary<int, int> mttrDic = new Dictionary<int, int>();
                foreach (string s in lines)
                {
                    string[] datas = s.Split(';');
                    mttrDic.Add(int.Parse(datas[0]), int.Parse(datas[1]));
                }
                return (Response.AsJson(new Response.Histogram()
                {
                    Code = 200,
                    Value = mttrDic
                }));
            });
        }
    }
}
