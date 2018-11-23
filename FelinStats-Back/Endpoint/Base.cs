using Nancy;

namespace FelinStats_Back.Endpoint
{
    public class Base : NancyModule
    {
        public Base()
        {
            Get("/", x =>
            {
                return (Response.AsJson(new Response.Sample()
                {
                    Code = 200,
                    Message = "Sample message, nothing to be done here..."
                }));
            });
        }
    }
}
