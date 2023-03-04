using EditorsCopilot.Foundation.OpenAI.Core;
using EditorsCopilot.Foundation.OpenAI.Core.Core.Interfaces.Security;
using EditorsCopilot.Foundation.OpenAI.Core.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test
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
            Assert.IsTrue(text.Length > 0);
        }


        [TestMethod]
        public void Can_Provide_Description()
        {
            var text = Api.TextService.GetDescription("Arab Emirates");
            Assert.IsNotNull(text);
            Assert.IsTrue(text.Length > 0);
        }

        [TestMethod]
        public void Can_Provide_Full_Text()
        {
            var text = Api.TextService.GetFullText("Arab Emirates");
            Assert.IsNotNull(text);
            Assert.IsTrue(text.Length > 0);
        }
    }
}
