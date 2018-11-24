using Nancy;

namespace FelinStats_Back.Endpoint
{
    public class Base : NancyModule
    {
        public Base()  : base("/")
        {
            Get("/", x =>
            {
                return (Response.AsJson(new Response.Error()
                {
                    Code = 200,
                    Message = "Available endpoints: mttr.json, mtbf.json, mtbfNow.json, serviceTime.json, weapon.json"
                })
                .WithHeader("Access-Control-Allow-Origin", "*")
                .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type"));
            });
        }
    }
}
