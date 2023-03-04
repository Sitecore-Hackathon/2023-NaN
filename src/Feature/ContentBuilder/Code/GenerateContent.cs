using System;
using EditorsCopilot.Feature.ContentBuilder.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Data;
using Sitecore.Data.Events;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.DependencyInjection;
using Sitecore.Events;
using Sitecore.Globalization;
using Sitecore.SecurityModel;

namespace EditorsCopilot.Feature.ContentBuilder.Core
{
    public class GenerateContent
    {

        private MediaItemService mediaItemService = new MediaItemService();
        
        public void OnItemAdded(object sender, EventArgs args)
        {
            //SitecoreEventArgs sitecoreEventArgs = args as SitecoreEventArgs;
            //if (!(Event.ExtractParameter(args, 0) is ItemCreatingEventArgs parameter) )
            //    return;
            //if (sitecoreEventArgs != null)
            //    sitecoreEventArgs.Result.Cancel = true;

            if (args == null)
                return;

            var item = Event.ExtractParameter(args, 0) as Item;
            if (item == null)
                return;

            var service = ServiceLocator.ServiceProvider.GetService<IItemContentService>();
            service?.PopulateItemContent(item.Name, item);
        }
    }
}