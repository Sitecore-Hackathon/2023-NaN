using EditorsCopilot.Foundation.OpenAI.Core;
using EditorsCopilot.Foundation.OpenAI.Core.Core.Interfaces.Security;
using EditorsCopilot.Foundation.OpenAI.Core.Core.Models;

namespace EditorsCopilot.Foundation.OpenAI.Test
{

    [TestClass]
    public class OpenApiTest
    {
        private IOpenAiCredentials credentials;
        private OpenAPI Api;

        [TestInitialize]
        public void TestInitialize()
        {
            credentials = new OpenAiCredentials()
            {
                ApiToken = "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",
            };

            Api = new OpenAPI(credentials);
        }


        [TestMethod]
        public void Can_Get_text()
        {
            var text = Api.TextService.GetTitle("");
            Assert.IsNotNull(text);
        }

        [TestMethod]
        public void Can_Provide_Valid_Title()
        {
            var text = Api.TextService.GetTitle("Arab Emirates");
            Assert.IsNotNull(text);
            Assert.IsTrue(text.Length < 255);
        }
    }
}