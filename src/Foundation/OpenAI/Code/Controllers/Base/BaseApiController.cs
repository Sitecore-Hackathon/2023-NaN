using OpenAI_API;
using System;

namespace EditorsCopilot.Foundation.OpenAI.Core.Controllers.Base
{
    public abstract class BaseApiController<T> where T : class
    {
        protected OpenAIAPI Client { get; private set; }
        
        protected BaseApiController(OpenAIAPI client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            Client = client;
        }
    }
}
