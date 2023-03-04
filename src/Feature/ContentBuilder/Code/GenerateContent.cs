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

            if (!item.Paths.FullPath.StartsWith("/sitecore/content"))
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
                                // TODO: generate image 
                                var url = "https://oaidalleapiprodscus.blob.core.windows.net/private/org-M5Ll0oTYLwDDPGNfDjFXe4yx/user-EhpUzsMXavmdjRzD6Okv0wTt/img-AYCZtKqj790G0sl01NSFFcV3.png?st=2023-03-04T18%3A42%3A59Z&se=2023-03-04T20%3A42%3A59Z&sp=r&sv=2021-08-06&sr=b&rscd=inline&rsct=image/png&skoid=6aaadede-4fb3-4698-a8f6-684d7786b067&sktid=a48cca56-e6da-484e-a814-9c849652bcb3&skt=2023-03-04T08%3A05%3A08Z&ske=2023-03-05T08%3A05%3A08Z&sks=b&skv=2021-08-06&sig=ks0tZoq4a/QCL4zcqYVfEtr4Jk51gZgxAVKbbBYk4Oc%3D";
                                var media = mediaItemService.UrlToMediaItem(url, item.Database);
                                if (media != null)
                                {
                                    ImageField imageField = item.Fields[field.Key];
                                    imageField.MediaID = media.ID;
                                }
                                
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