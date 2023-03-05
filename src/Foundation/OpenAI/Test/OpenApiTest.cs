using EditorsCopilot.Foundation.OpenAI.Core;
using EditorsCopilot.Foundation.OpenAI.Core.Core.Interfaces.Security;
using EditorsCopilot.Foundation.OpenAI.Core.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using EditorsCopilot.Foundation.OpenAI.Core.Core;

namespace Test
{
    [TestClass]
    public class OpenApiTest
    {
        private IOpenAiCredentials credentials;
        private OpenAPI Api;

        private readonly PhrasesConfiguration _config = new PhrasesConfiguration()
        {
            TitlePhrase = "Explain one sentence",
            DescriptionPhrase = "Explain in a few sentences",
            FullTextPhrase = "Explain",
            StartedWith = "started with"
        };

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
            var text = Api.TextService.GetTitle(_config,"");
            Assert.IsNotNull(text);
        }

        [TestMethod]
        public void Can_Provide_Valid_Title()
        {
            var text = Api.TextService.GetTitle(_config, "Arab Emirates");
            Assert.IsNotNull(text);
            Assert.IsTrue(text.Length > 0);
        }


        [TestMethod]
        public void Can_Provide_Description()
        {
            var text = Api.TextService.GetDescription(_config, "Arab Emirates");
            Assert.IsNotNull(text);
            Assert.IsTrue(text.Length > 0);
        }

        [TestMethod]
        public void Can_Provide_Full_Text()
        {
            var text = Api.TextService.GetFullText(_config, "Arab Emirates");
            Assert.IsNotNull(text);
            Assert.IsTrue(text.Length > 0);
        }
    }
}