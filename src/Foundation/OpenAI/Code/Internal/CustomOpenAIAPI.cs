using EditorsCopilot.Foundation.OpenAI.Core.Internal.Images;
using OpenAI_API;

namespace EditorsCopilot.Foundation.OpenAI.Core.Internal
{
    public class CustomOpenAIAPI : OpenAIAPI
    {
        public ImagesEndpoint Images { get; }

        public CustomOpenAIAPI(APIAuthentication apiKeys = null) : base(apiKeys)
        {
            Images = new ImagesEndpoint(this);
        }
    }
}