﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorsCopilot.Foundation.OpenAI.Core.Core.Interfaces.Controllers
{
    public interface IOpenAiTextController
    {
        bool IsValid { get; }
        string GetTitle(string text);

        string GetDescription(string text);
        
        string GetFullText(string text);

        string GetImageUrl(string text);
    }
}