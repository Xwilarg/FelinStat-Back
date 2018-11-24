﻿using Nancy;

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
                    Message = "Available endpoints: Mttr"
                }));
            });
        }
    }
}
