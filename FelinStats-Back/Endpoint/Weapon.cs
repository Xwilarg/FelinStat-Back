using FelinStats_Back.Response;
using Nancy;
using System.Collections.Generic;
using System.IO;

namespace FelinStats_Back.Endpoint
{
    public class Weapon : NancyModule
    {
        public Weapon() : base("/weapon.json")
        {
            // Time between 2 failures
            Get("/", x =>
            {
                if (!File.Exists("weapon.txt"))
                    return (Response.AsJson(new Response.Error()
                    {
                        Code = 503,
                        Message = "weapon.txt doesn't exist."
                    })
                    .WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type"));
                string id = Request.Query["id"];
                if (string.IsNullOrWhiteSpace(id))
                    return (Response.AsJson(new Error
                    {
                        Code = 400,
                        Message = "You must provide the 'id' argument"
                    })
                    .WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type"));
                string[] lines = File.ReadAllLines("weapon.txt");
                List<WeaponElement> elems = new List<WeaponElement>();
                foreach (string s in lines)
                {
                    string[] datas = s.Split(';');
                    if (datas[0] == id)
                    {
                        elems.Add(new WeaponElement()
                        {
                            Date = datas[1],
                            Applicant = datas[2],
                            Repairor = datas[3],
                            InterventionLevel = datas[4],
                            ItType = datas[5]
                        });
                    }
                }
                return (Response.AsJson(new Response.Weapon()
                {
                    Code = 200,
                    Elems = elems.ToArray()
                })
                .WithHeader("Access-Control-Allow-Origin", "*")
                .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type"));
            });
        }
    }
}
