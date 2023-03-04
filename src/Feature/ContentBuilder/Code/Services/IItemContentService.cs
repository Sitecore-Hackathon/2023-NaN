using Sitecore.Data.Items;

namespace EditorsCopilot.Feature.ContentBuilder.Core.Services
{
    public interface IItemContentService
    {
        void PopulateItemContent(Item item);
    }
}