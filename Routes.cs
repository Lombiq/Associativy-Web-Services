using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Mvc.Routes;

namespace Associativy.WebServices
{
    public class Routes : IRouteProvider
    {
        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
        }

        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[]
            {
                new RouteDescriptor
                {
                    Name = "Associativy.WebServices.ServiceRoute",
                    Route = new Route(
                        "Associativy/WebServices/Nodes/{id}",
                        new RouteValueDictionary {
                                                    {"area", "Associativy.WebServices"},
                                                    {"controller", "Nodes"},
                                                    {"action", "Index"},
                                                    {"id", "0"}
                                                },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                                                    {"area", "Associativy.WebServices"}
                                                },
                        new MvcRouteHandler())
                }
            };
        }
    }
}
