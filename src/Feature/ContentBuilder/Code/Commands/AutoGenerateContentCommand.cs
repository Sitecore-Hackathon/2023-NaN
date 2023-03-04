using EditorsCopilot.Feature.ContentBuilder.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.DependencyInjection;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;

namespace EditorsCopilot.Feature.ContentBuilder.Core.Commands
{
    public class AutoGenerateContentCommand : Command
    {
        public override void Execute(CommandContext context)
        {
            if (context.Items.Length == 1)
            {
                var item = context.Items[0];
                var parameters = new
                    System.Collections.Specialized.NameValueCollection
                    {
                        ["itemUri"] = item.Uri.ToString(),
                    };
                Context.ClientPage.Start(this, "Run", parameters);
            }
        }

        protected void Run(ClientPipelineArgs args)
        {
            if (args.IsPostBack)
            {
                if ((!string.IsNullOrEmpty(args.Result)) && args.Result != "undefined")
                {
                    var item = GetItem(args);
                    if (item != null)
                    {
                        var topic = args.Result;
                        Log.Info($"Generate content for item '{item.ID}' and topic '{args.Result}'", this);
                        var service = ServiceLocator.ServiceProvider.GetService<IItemContentService>();
                        service?.PopulateItemContent(topic, item);
                        Context.ClientPage.ClientResponse.Timer("item:load(id=" + item.ID + ")", 0);
                    }
                }
            }
            else
            {
                var item = GetItem(args);
                SheerResponse.Input("Please enter topic for your item", item?.Name ?? string.Empty);
                args.WaitForPostBack();
            }
        }

        private static Item GetItem(ClientPipelineArgs args)
        {
            var uriParam = args.Parameters["itemUri"];
            if (ItemUri.IsItemUri(uriParam))
            {
                var uri = ItemUri.Parse(uriParam);
                return Database.GetItem(uri);
            }

            return null;
        }
    }
}