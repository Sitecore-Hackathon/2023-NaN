using OpenAI_API;
using System;
using EditorsCopilot.Foundation.OpenAI.Core.Internal;

namespace EditorsCopilot.Foundation.OpenAI.Core.Controllers.Base
{
    public abstract class BaseApiController<T> where T : class
    {
        protected CustomOpenAIAPI Client { get; private set; }
        
        protected BaseApiController(CustomOpenAIAPI client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            Client = client;
        }
    }
}
