using System;
using EditorsCopilot.Feature.ContentBuilder.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.DependencyInjection;
using Sitecore.Events;

namespace EditorsCopilot.Feature.ContentBuilder.Core
{
    public class GenerateContent
    {
        private MediaItemService mediaItemService = new MediaItemService();

        public void OnItemAdded(object sender, EventArgs args)
        {
            if (args == null)
                return;

            var item = Event.ExtractParameter(args, 0) as Item;
            if (item == null)
                return;


            if (!item.Paths.FullPath.StartsWith("/sitecore/content"))
                return;

            var module = item.Database.GetItem(Constants.Items.Module);

            CheckboxField generateGlobal = module.Fields[Constants.Fields.GerenateContent];
            bool gerenateContent = generateGlobal.Checked;

            if (!gerenateContent)
            {
                CheckboxField enableOnItem = item.Fields[Constants.Fields.EnableAiGeneration];

                if (enableOnItem != null && enableOnItem.Checked)
                {
                    gerenateContent = true;
                }
            }

            if (gerenateContent)
            {
                var service = ServiceLocator.ServiceProvider.GetService<IItemContentService>();
                service?.PopulateItemContent(item.Name, item);
            }

        }
    }
}