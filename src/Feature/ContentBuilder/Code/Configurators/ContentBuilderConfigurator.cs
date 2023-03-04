using EditorsCopilot.Feature.ContentBuilder.Core.Services;
using EditorsCopilot.Foundation.OpenAI.Core.Core.Interfaces.Security;
using EditorsCopilot.Foundation.OpenAI.Core.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Sitecore;
using Sitecore.Data;
using Sitecore.DependencyInjection;

namespace EditorsCopilot.Feature.ContentBuilder.Core.Configurators
{
    public class ContentBuilderConfigurator: IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IOpenAiCredentials>(s =>
            {
                var db = Database.GetDatabase(Constants.ModuleDatabase);
                return new OpenAiCredentials()
                {
                    ApiToken = db.GetItem(Constants.Items.Module)?.Fields[Constants.Fields.OpenAIKey]?.Value,
                };
            });
            serviceCollection.AddSingleton<IItemContentService, ItemContentService>();
        }
    }
}