using System.Linq;
using EditorsCopilot.Foundation.OpenAI.Core.Controllers.Base;
using EditorsCopilot.Foundation.OpenAI.Core.Core.Interfaces.Controllers;
using EditorsCopilot.Foundation.OpenAI.Core.Internal;
using EditorsCopilot.Foundation.OpenAI.Core.Internal.Images;
using EditorsCopilot.Foundation.OpenAI.Core.Tools;
using OpenAI_API.Completions;
using OpenAI_API.Models;

namespace EditorsCopilot.Foundation.OpenAI.Core.Controllers
{
    public class OpenAiTextController : BaseApiController<OpenAiTextController>,
        IOpenAiTextController
    {
        public const double Temperature = 0.7;
        
        public OpenAiTextController(CustomOpenAIAPI client)
            : base(client)
        {
        }

        public bool IsValid => !string.IsNullOrEmpty(Client.Auth.ApiKey);

        public string GetTitle(string text)
        {
            var result = AsyncHelper.RunSync(() => Client.Completions.CreateCompletionAsync(
                new CompletionRequest($"Explain one sentence: '{text}'",
                    model: Model.CurieText, temperature: Temperature, max_tokens: 257)));

            return result.ToString().Trim();
        }

        public string GetDescription(string text)
        {
            var result = AsyncHelper.RunSync(() => Client.Completions.CreateCompletionAsync(
                new CompletionRequest($"Explain in a few sentences : '{text}'",
                    model: Model.CurieText, temperature: Temperature, max_tokens: 512)));

            return result.ToString().Trim();
        }

        public string GetFullText(string text)
        {
            var result = AsyncHelper.RunSync(() => Client.Completions.CreateCompletionAsync(
                new CompletionRequest($"Explain: {text}",
                    model: Model.CurieText, temperature: Temperature, max_tokens: 1027)));

            return result.ToString().Trim();
        }

        public string GetImageUrl(string text)
        {
            var result = AsyncHelper.RunSync(() => Client.Images.RequestImages(
                new ImagesRequest(text)));

            return result?.Data?.FirstOrDefault()?.Url;
        }
    }
}