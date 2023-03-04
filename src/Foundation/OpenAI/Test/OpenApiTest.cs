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


        [TestMethod]
        public void Can_Provide_Description()
        {
            var text = Api.TextService.GetDescription("Arab Emirates");
            Assert.IsNotNull(text);
            Assert.IsTrue(text.Length < 255);
        }

        [TestMethod]
        public void Can_Provide_Full_Text()
        {
            var text = Api.TextService.GetFullText("Arab Emirates");
            Assert.IsNotNull(text);
            Assert.IsTrue(text.Length < 255);
        }
    }
}