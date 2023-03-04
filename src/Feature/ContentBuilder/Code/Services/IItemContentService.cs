using Sitecore.Data.Items;

namespace EditorsCopilot.Feature.ContentBuilder.Core.Services
{
    public interface IItemContentService
    {
        void PopulateItemContent(string topic, Item item);
    }
}