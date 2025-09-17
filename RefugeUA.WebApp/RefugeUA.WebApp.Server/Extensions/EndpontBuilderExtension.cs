using RefugeUA.WebApp.Server.Features;

namespace RefugeUA.WebApp.Server.Extensions
{
    public static class EndpointBuilderExtension
    {
        public static void CreateEndpoint<TFeatureEndpoint>(this IEndpointRouteBuilder app)
            where TFeatureEndpoint : IFeatureEndpoint,
            new()
        {
            TFeatureEndpoint featureEndpoint = new TFeatureEndpoint();
            featureEndpoint.AddEndpoint(app);
        }

        public static void MapAllEndpointsFromCurrentAssembly(this IEndpointRouteBuilder app)
        {
            var featureEndpoints = typeof(Program).Assembly
                .GetTypes()
                .Where(t => typeof(IFeatureEndpoint).IsAssignableFrom(t) && !t.IsAbstract)
                .Select(t => (IFeatureEndpoint)Activator.CreateInstance(t)!)
                .ToList();

            foreach (var featureEndpoint in featureEndpoints)
            {
                featureEndpoint.AddEndpoint(app);
            }
        }
    }
}
