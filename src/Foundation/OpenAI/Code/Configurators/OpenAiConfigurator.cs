using EditorsCopilot.Foundation.OpenAI.Core.Core.Interfaces.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace EditorsCopilot.Foundation.OpenAI.Core.Configurators
{
    public class OpenAiConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<OpenAPI>();
            serviceCollection.AddTransient<IOpenAiTextController>(s => s.GetService<OpenAPI>()?.TextService);
        }
    }
}