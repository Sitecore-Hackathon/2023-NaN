using EditorsCopilot.Foundation.OpenAI.Core.Abstract;
using EditorsCopilot.Foundation.OpenAI.Core.Controllers;
using EditorsCopilot.Foundation.OpenAI.Core.Core.Interfaces.Controllers;
using EditorsCopilot.Foundation.OpenAI.Core.Core.Interfaces.Security;

namespace EditorsCopilot.Foundation.OpenAI.Core
{
    public class OpenAPI : OpenApiConnector
    {
        public OpenAPI(IOpenAiCredentials credentials)
            : base(credentials)
        {
            RegisterControllers();
        }

        private void RegisterControllers()
        {
            Registration(() => new OpenAiTextController(Client));
        }

        public IOpenAiTextController TextService => this.GetController<OpenAiTextController>();
    }
}