using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace EditorsCopilot.Feature.ContentBuilder.Core.Services
{
    public class ItemContentService : IItemContentService
    {
        public void PopulateItemContent(Item item)
        {
            Log.Info("Here item content should be populated", this);
        }
    }
}