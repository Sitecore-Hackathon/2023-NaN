using System;
using System.Collections.Generic;
using EditorsCopilot.Foundation.OpenAI.Core.Core.Interfaces.Security;
using OpenAI_API;

namespace EditorsCopilot.Foundation.OpenAI.Core.Abstract
{
    public abstract class OpenApiConnector
    {
        protected OpenAIAPI Client { get; private set; }
        protected IOpenAiCredentials Options { get; private set; }

        public OpenApiConnector(IOpenAiCredentials credentials)
        {
            if (credentials == null)
                throw new ArgumentNullException(nameof(credentials));

            if (string.IsNullOrWhiteSpace(credentials.ApiToken))
                throw new ArgumentNullException(nameof(credentials.ApiToken));

            Options = credentials;
            Client = new OpenAIAPI(new APIAuthentication(credentials.ApiToken));
        }
    }
}
