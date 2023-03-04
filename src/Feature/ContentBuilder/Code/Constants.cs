using Sitecore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorsCopilot.Feature.ContentBuilder.Core
{
    public static class Constants
    {
        public const string ModuleDatabase = "master";
        public static class Items
        {
            public static ID Module = new ID("{ED8EF05E-A872-45E8-9E62-D9AD84AB8A34}");
        }
        public static class Fields
        {
            public static ID GerenateContent = new ID("{10D06985-DADC-4DC7-986F-1219E5B47E2A}");
            public static ID OpenAIKey = new ID("{250D335A-D8C0-4D66-9E6F-B3B1A587F3BD}");
        }
    }
}
