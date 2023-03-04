using Sitecore.Data.Items;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Resources.Media;
using Sitecore.SecurityModel;
using System;
using System.IO;
using System.Linq;
using System.Net;
using Sitecore.Data.Events;

namespace EditorsCopilot.Feature.ContentBuilder.Core
{
    public  class MediaItemService
    {
        private string mediaLibraryPath = "/sitecore/media library/OpenAI/";

        public MediaItem UrlToMediaItem(string url, Database targetDatabase)
        {
            string fileName = new Uri(url).Segments.Last();
            var mediaItem = CreateMediaItemFromWebApi(url, targetDatabase, mediaLibraryPath, fileName);
            return mediaItem;
        }
        private MediaItem SaveFileToMediaLibraryFromStream(Stream stream, string itemName, string fileName, Database targetDatabase,
            string destinationPath, bool generateUniqueFileName)
        {
            var name = generateUniqueFileName
                ? GetItemName(Path.GetFileNameWithoutExtension(itemName))
                : Path.GetFileNameWithoutExtension(itemName);
            name = ItemUtil.ProposeValidItemName(name.Trim()).Replace('-', ' ');
            fileName = fileName.Trim();

            MediaCreatorOptions options = new MediaCreatorOptions
            {
                FileBased = false,
                IncludeExtensionInItemName = false,
                OverwriteExisting = true,
                Versioned = false,
                Database = targetDatabase,
                Destination = destinationPath + name,
            };

            try
            {
                using (new SecurityDisabler())
                {
                    MediaCreator creator = new MediaCreator();
                    MediaItem mediaItem = creator.CreateFromStream(stream, fileName, options);
                    return mediaItem;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Cannot create media item!", ex, this);
                return null;
            }
        }



        private MediaItem CreateMediaItemFromWebApi(string webApiRequest, Database targetDatabase, string itemPath, string itemName, bool unicName = true)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webApiRequest);

                var webResponse = request.GetResponse();

                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream == null)
                    {
                        throw new Exception(string.Format("Can't open stream to read WebAPI response. WebAPI request = {0}", webApiRequest));
                    }

                    using (var stream = new MemoryStream())
                    {
                        webStream.CopyTo(stream);

                        return this.SaveFileToMediaLibraryFromStream(stream, itemName, itemName, targetDatabase, itemPath, unicName);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Error in get MediaItem from WebAPI. WebAPI request = {webApiRequest}. Item Path = {itemPath}. Item Name = {itemName}.", ex, this);
                return null;
            }
        }

        private string GetItemName(string fileName)
        {
            return ItemUtil.ProposeValidItemName(string.Format("{0} {1}", DateTime.Now.ToString("yyyyMMdd hhmmss"), fileName));
        }
    }
}
