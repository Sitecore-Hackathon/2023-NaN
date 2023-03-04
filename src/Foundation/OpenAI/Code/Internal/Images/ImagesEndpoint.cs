using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenAI_API;

namespace EditorsCopilot.Foundation.OpenAI.Core.Internal.Images
{
    public class ImagesEndpoint: CustomEndpointBase
    {
        protected override string Endpoint => "images/generations";
        
        public async Task<ImagesResult> RequestImages(ImagesRequest request)
        {
            return await HttpPost<ImagesResult>(postData: request);
        }

        public ImagesEndpoint(OpenAIAPI api) : base(api)
        {
        }
    }

    public class ImagesRequest
    {
        public const string Small = "256x256";
        public const string Medium = "512x512";
        public const string Large = "1024x1024";

        [JsonProperty("prompt")]
        public string Prompt { get; set; }

        [JsonProperty("n")] public int ImagesCount { get; set; } = 1;

        [JsonProperty("size")] public string Size { get; set; } = Medium;

        public ImagesRequest(string prompt)
        {
            Prompt = prompt;
        }
    }

    public class ImagesResult: ApiResultBase
    {
        [JsonProperty("data")]
        public IList<Image> Data { get; set; }
    }

    public class Image
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}