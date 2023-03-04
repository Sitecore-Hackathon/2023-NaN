using EditorsCopilot.Foundation.OpenAI.Core.Core.Interfaces.Security;

namespace EditorsCopilot.Foundation.OpenAI.Core.Core.Models
{
    public class OpenAiCredentials : IOpenAiCredentials
    {
        public string ApiToken { get; set; }
    }
}
