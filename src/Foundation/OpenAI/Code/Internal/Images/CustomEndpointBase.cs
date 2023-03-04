using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenAI_API;

namespace EditorsCopilot.Foundation.OpenAI.Core.Internal.Images
{
    public abstract class CustomEndpointBase
    {
        private const string UserAgent = "okgodoit/dotnet_openai_api";
        
        protected readonly OpenAIAPI _Api;
        protected abstract string Endpoint { get; }
        
        protected string Url
        {
            get
            {
                return string.Format(_Api.ApiUrlFormat, _Api.ApiVersion, Endpoint);
            }
        }
        
        internal CustomEndpointBase(OpenAIAPI api)
        {
            this._Api = api;
        }
        
        internal async Task<T> HttpPost<T>(string url = null, object postData = null) where T : ApiResultBase
        {
            if (string.IsNullOrEmpty(url))
                url = Url;
            using (var client = GetClient())
            {
                var req = new HttpRequestMessage(HttpMethod.Post, url);
                if (postData != null)
                {
                    if (postData is HttpContent)
                    {
                        req.Content = postData as HttpContent;
                    }
                    else
                    {
                        string jsonContent = JsonConvert.SerializeObject(postData,
                            new JsonSerializerSettings() {NullValueHandling = NullValueHandling.Ignore});
                        var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                        req.Content = stringContent;
                    }
                }

                var resp = await client.SendAsync(req);
                string resultAsString = await resp.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(resultAsString);
            }
        }
        
        protected HttpClient GetClient()
        {
            if (_Api.Auth?.ApiKey is null)
            {
                throw new AuthenticationException("You must provide API authentication.  Please refer to https://github.com/OkGoDoIt/OpenAI-API-dotnet#authentication for details.");
            }

            /*
            if (_Api.SharedHttpClient==null)
            {
                _Api.SharedHttpClient = new HttpClient();
                _Api.SharedHttpClient.
            }
            */

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _Api.Auth.ApiKey);
            // Further authentication-header used for Azure openAI service
            client.DefaultRequestHeaders.Add("api-key", _Api.Auth.ApiKey);
            client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            if (!string.IsNullOrEmpty(_Api.Auth.OpenAIOrganization)) client.DefaultRequestHeaders.Add("OpenAI-Organization", _Api.Auth.OpenAIOrganization);

            return client;
        }
    }
}