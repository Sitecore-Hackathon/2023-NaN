using System;
using EditorsCopilot.Foundation.OpenAI.Core.Core;
using EditorsCopilot.Foundation.OpenAI.Core.Core.Interfaces.Controllers;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.SecurityModel;

namespace EditorsCopilot.Feature.ContentBuilder.Core.Services
{
    public class ItemContentService : IItemContentService
    {
        private readonly IOpenAiTextController _openAi;
        private readonly MediaItemService _mediaItemService = new MediaItemService();

        public ItemContentService(IOpenAiTextController openAi)
        {
            _openAi = openAi;
        }

        public void PopulateItemContent(string topic, Item item)
        {
            try
            {
                PopulateItemContentInternal(topic, item);
            }
            catch (Exception e)
            {
                Log.Error($"Error generate content", e, this);
            }
        }

        private void PopulateItemContentInternal(string topic, Item item)
        {
            var ownFieldCollection = item.Template.OwnFields;
            var config = GetConfig(item.Language);
            using (new SecurityDisabler())
            {
                using (new EditContext(item))
                {
                    foreach (var field in ownFieldCollection)
                    {
                        var type = FieldTypeManager.GetField(item.Fields[field.Key]);
                        if (type != null)
                        {
                            if (type is TextField)
                            {
                                item[field.Key] = _openAi.GetTitle(config, topic);
                            }
                            else if (type is HtmlField)
                            {
                                item[field.Key] = _openAi.GetDescription(config, topic);
                            }
                            else if (type is ImageField)
                            {
                                var url = _openAi.GetImageUrl(config, topic);
                                if (!string.IsNullOrEmpty(url))
                                {
                                    var media = _mediaItemService.UrlToMediaItem(url, item.Database);
                                    if (media != null)
                                    {
                                        ImageField imageField = item.Fields[field.Key];
                                        imageField.MediaID = media.ID;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private PhrasesConfiguration GetConfig(Language language)
        {
            var db = Database.GetDatabase(Constants.ModuleDatabase);
            var item = db.GetItem(Constants.Items.Module, language);
            return new PhrasesConfiguration()
            {
                TitlePhrase = item?.Fields[Constants.Fields.TitlePhrase]?.Value ?? Constants.DefaultValues.TitlePhrase,
                DescriptionPhrase = item?.Fields[Constants.Fields.DescriptionPhrase]?.Value ??
                                    Constants.DefaultValues.DescriptionPhrase,
                FullTextPhrase = item?.Fields[Constants.Fields.FullTextPhrase]?.Value ??
                                 Constants.DefaultValues.FullTextPhrase,
                StartedWith = item?.Fields[Constants.Fields.StartWith]?.Value ??
                              Constants.DefaultValues.StartWith,
                Language = language.Name,
            };
        }
    }
}