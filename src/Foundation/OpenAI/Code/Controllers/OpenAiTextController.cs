using System.Linq;
using EditorsCopilot.Foundation.OpenAI.Core.Controllers.Base;
using EditorsCopilot.Foundation.OpenAI.Core.Core.Interfaces.Controllers;
using EditorsCopilot.Foundation.OpenAI.Core.Internal;
using EditorsCopilot.Foundation.OpenAI.Core.Internal.Images;
using EditorsCopilot.Foundation.OpenAI.Core.Tools;
using OpenAI_API.Completions;
using OpenAI_API.Models;
using Sitecore.Diagnostics;

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
                new CompletionRequest(CompletionRequestQuery("Explain one sentence", text),
                    model: Model.CurieText, temperature: Temperature, max_tokens: 257)));
            var final = result.ToString().Trim();
            LogMessage(nameof(GetTitle), text, final);
            return final;
        }

        public string GetDescription(string text)
        {
            var result = AsyncHelper.RunSync(() => Client.Completions.CreateCompletionAsync(
                new CompletionRequest(CompletionRequestQuery("Explain in a few sentences", text),
                    model: Model.CurieText, temperature: Temperature, max_tokens: 512)));
            var final = result.ToString().Trim();
            LogMessage(nameof(GetDescription), text, final);
            return final;
        }

        public string GetFullText(string text)
        {
            var result = AsyncHelper.RunSync(() => Client.Completions.CreateCompletionAsync(
                new CompletionRequest(CompletionRequestQuery("Explain", text),
                    model: Model.CurieText, temperature: Temperature, max_tokens: 1027)));
            var final = result.ToString().Trim();
            LogMessage(nameof(GetFullText), text, final);
            return final;
        }

        public string GetImageUrl(string text)
        {
            var result = AsyncHelper.RunSync(() => Client.Images.RequestImages(
                new ImagesRequest(text)));
            var url = result?.Data?.FirstOrDefault()?.Url;
            LogMessage(nameof(GetImageUrl), text, url);
            return result?.Data?.FirstOrDefault()?.Url;
        }

        private void LogMessage(string method, string text, string result)
        {
            Log.Info($"[OpenAi] {method}: generated from '{text}' -> '{result}'", this);
        }
        
        private static string CompletionRequestQuery(string command, string text)
        {
            return string.Format(command + " '{0}', started with '{0}'", text);
        }
    }
}