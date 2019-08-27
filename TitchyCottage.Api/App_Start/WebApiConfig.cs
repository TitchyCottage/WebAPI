using Newtonsoft.Json.Serialization;
using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace TitchyCottage.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //config.EnableSwagger(c => c.SingleApiVersion("v1", "TitchyCottage.Api")).EnableSwaggerUi();
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // This is the call to our swashbuckle config that needs to be called
            //SwaggerConfig.Register(config);

            // invoke the web API 
            //application.UseWebApi(config);

        }
    }
}
