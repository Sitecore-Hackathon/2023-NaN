using System;
using Sitecore.Data;
using Sitecore.Data.Events;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Events;
using Sitecore.Globalization;
using Sitecore.SecurityModel;

namespace EditorsCopilot.Feature.ContentBuilder.Core
{
    public class GenerateContent
    {

       // protected IContext Context { get; } = ServiceLocator.ServiceProvider.GetService<IContext>();

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

            var module = item.Database.GetItem(Constants.Items.Module);
            CheckboxField generateContent = module.Fields[Constants.Fields.GerenateContent];
            if (generateContent.Checked)
            {
                var text = item.Name;
                // TODO: generate text and image for each custom field

                var ownFieldCollection = item.Template.OwnFields;
                using (new SecurityDisabler())
                {
                    item.Editing.BeginEdit();
                    foreach (var field in ownFieldCollection)
                    {
                        var type = FieldTypeManager.GetField(item.Fields[field.Key]);
                        if (type != null)
                        {
                            if (type is LinkField)
                            {

                            }

                            else if (type is TextField)
                            {
                                item[field.Key] = "it works";
                            }
                            else if (type is ImageField)
                            {

                            }
                        }
                        //. string value = item.Fields[field.Key].Value;
                        // or do whatever here
                    }
                    item.Editing.EndEdit();
                }
               
            }


            


            //this.Context.ClientPage.ClientResponse.Alert(Translate.Text("You are not allowed to create any items here."));
        }
    }
}