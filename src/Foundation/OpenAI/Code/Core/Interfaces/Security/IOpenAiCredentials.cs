using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorsCopilot.Foundation.OpenAI.Core.Core.Interfaces.Security
{
    public interface IOpenAiCredentials
    {
        string ApiToken { get; set; }
    }
}