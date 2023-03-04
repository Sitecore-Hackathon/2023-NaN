using System.Collections.Generic;
using System.Linq;
using EditorsCopilot.Foundation.OpenAI.Core.Core.Interfaces.Controllers;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Fields;
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

        protected void Run(Sitecore.Web.UI.Sheer.ClientPipelineArgs args)
        {
            if (args.IsPostBack)
            {
                if ((!string.IsNullOrEmpty(args.Result)) && args.Result != "undefined")
                {
                    var uriParam = args.Parameters["itemUri"];
                    if (ItemUri.IsItemUri(uriParam))
                    {
                        var uri = ItemUri.Parse(uriParam);
                        var item = Database.GetItem(uri);
                        var topic = args.Result;
                        Log.Info($"Generate content for item '{item.ID}' and topic '{args.Result}'", this);
                        var service = (IOpenAiTextController) ServiceLocator.ServiceProvider.GetService(typeof(IOpenAiTextController));
                        Log.Info($"OpenAI result: title = '{service?.GetTitle(topic)}'", this);
                        Context.ClientPage.SendMessage(this, "item:load(id=" + item.ID + ")");
                    }
                }
            }
            else
            {
                SheerResponse.Input("Please enter topic for your item", string.Empty);
                args.WaitForPostBack();
            }
        }

        protected void RunGenerateForNextField(ClientPipelineArgs args)
        {
            if ((!string.IsNullOrEmpty(args.Result)) && args.Result != "undefined")
            {
                var uriParam = args.Parameters["itemUri"];
                if (ItemUri.IsItemUri(uriParam))
                {
                    var uri = ItemUri.Parse(uriParam);
                    var item = Database.GetItem(uri);
                    Log.Info($"Generate content for item '{item.ID}' and topic '{args.Result}'", this);
                    Context.ClientPage.SendMessage(this, "item:load(id=" + item.ID + ")");
                }
            }
        }

        private IList<Field> GetFields(Item item)
        {
            return item.Fields.Where(x => !InternalFields.Contains(x.ID)).ToList();
        }

        protected static readonly ID[] InternalFields = new ID[]
        {
            FieldIDs.Created,
            FieldIDs.CreatedBy,
            FieldIDs.Updated,
            FieldIDs.UpdatedBy,
            FieldIDs.ValidFrom,
            FieldIDs.ValidTo,
            FieldIDs.WorkflowState,
            FieldIDs.Lock,
            FieldIDs.Revision,
            FieldIDs.ReminderDate,
            FieldIDs.ReminderRecipients,
            FieldIDs.ReminderText,
            FieldIDs.HideVersion,
            FieldIDs.Owner
        };
    }
}