using Microsoft.Extensions.Logging;
using OpenAI_API;

namespace EditorsCopilot.Foundation.OpenAI.Core.Controllers.Base
{
    public abstract class BaseApiController<T> where T : class
    {
        protected OpenAIAPI Client { get; private set; }
        
        protected ILogger<T> Logger { get; private set; }

        protected BaseApiController(OpenAIAPI client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            Logger = LoggerFactory.Create(options => { }).CreateLogger<T>()
                ?? throw new ArgumentNullException(nameof(Logger));

            Client = client;
        }
    }
}
