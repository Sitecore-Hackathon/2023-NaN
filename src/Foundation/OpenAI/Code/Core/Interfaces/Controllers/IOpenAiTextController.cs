using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorsCopilot.Foundation.OpenAI.Core.Core.Interfaces.Controllers
{
    public interface IOpenAiTextController
    {
        bool IsValid { get; }
        string GetTitle(PhrasesConfiguration config, string text);

        string GetDescription(PhrasesConfiguration config, string text);
        
        string GetFullText(PhrasesConfiguration config, string text);

        string GetImageUrl(PhrasesConfiguration config, string text);
    }
}