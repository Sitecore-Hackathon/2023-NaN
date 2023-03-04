using EditorsCopilot.Foundation.OpenAI.Core.Controllers.Base;
using EditorsCopilot.Foundation.OpenAI.Core.Core.Interfaces.Controllers;
using EditorsCopilot.Foundation.OpenAI.Core.Tools;
using OpenAI_API;
using OpenAI_API.Completions;
using OpenAI_API.Models;

namespace EditorsCopilot.Foundation.OpenAI.Core.Controllers
{
    public class OpenAiTextController : BaseApiController<OpenAiTextController>,
        IOpenAiTextController
    {
        public OpenAiTextController(OpenAIAPI client) 
            : base(client)
        {
            
        }

        public bool IsValid => !string.IsNullOrEmpty(Client.Auth.ApiKey);

        public string GetTitle(string text)
        {
            var result = AsyncHelper.RunSync(() => Client.Completions.CreateCompletionAsync(
                new CompletionRequest($"continue the headline: {text}",
                    model: Model.CurieText, temperature: 0.1)));

            return result.ToString().Trim();
        }

        public string GetDescription(string text)
        {
            var result = AsyncHelper.RunSync(() => Client.Completions.CreateCompletionAsync(
                new CompletionRequest($"continue short description: {text}",
                    model: Model.CurieText, temperature: 0.1)));

            return result.ToString().Trim();
        }

        public string GetFullText(string text)
        {
            var result = Client.Completions.CreateCompletionAsync(
                new CompletionRequest($"explain: {text}",
                    model: Model.CurieText, temperature: 0.1)).Result;

            return result.ToString().Trim();
        }
    }
}