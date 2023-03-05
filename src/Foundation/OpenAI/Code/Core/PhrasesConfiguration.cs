namespace EditorsCopilot.Foundation.OpenAI.Core.Core
{
    public class PhrasesConfiguration
    {
        public string TitlePhrase { get; set; }
        public string DescriptionPhrase { get; set; }
        public string FullTextPhrase { get; set; }
        public string StartedWith { get; set; }
        public string Language { get; set; } = "en";
    }
}