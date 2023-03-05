using Sitecore.Data;

namespace EditorsCopilot.Feature.ContentBuilder.Core
{
    public static class Constants
    {
        public const string ModuleDatabase = "master";
        public static class Items
        {
            public static ID Module = new ID("{ED8EF05E-A872-45E8-9E62-D9AD84AB8A34}");
        }

        public static class Templates
        {
            public static ID Copilot = new ID("{E7C47589-124E-4B1F-A4C7-255564DC474C}");
        }
        public static class Fields
        {
            public static ID GerenateContent = new ID("{10D06985-DADC-4DC7-986F-1219E5B47E2A}");
            public static ID OpenAIKey = new ID("{250D335A-D8C0-4D66-9E6F-B3B1A587F3BD}");
            public static ID EnableAiGeneration = new ID("{E3620672-833E-4EF2-97C3-EE25963ED7CD}");
            public static ID TitlePhrase = new ID("{14F30FF3-F8FD-4BE4-A4FA-1631D1E1DC3F}");
            public static ID DescriptionPhrase = new ID("{7F8C6F6F-4222-4908-8D94-9ADEE2904957}");
            public static ID FullTextPhrase = new ID("{E2C7DB2D-CD31-4908-A08D-2A678C0D12C1}");
            public static ID StartWith = new ID("{E5FBB787-7565-457D-976A-DF511C2DFB85}");
        }

        public static class DefaultValues
        {
            public const string TitlePhrase = "Explain one sentence";
            public const string DescriptionPhrase = "Explain in a few sentences";
            public const string FullTextPhrase = "Explain";
            public const string StartWith = "started with";
        }
    }
}
