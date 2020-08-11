﻿using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualSociety.VirtualSocietyDid;
using Vs.Definitions.Models;
using Vs.Definitions.Repositories;
using Vs.Morstead.Grains.Interfaces.Content;
using Vs.Morstead.Grains.Interfaces.Primitives.Directory;

namespace Vs.ProfessionalPortal.Morstead.Client.Controllers
{
    public class YamlStorageRepository : IFileRepository
    {
        private static string GlobalDid = "did:vsoc:mstd:dir:UtF0tMaRjUqzW4PjMjtr8g";

        private string Did { get; set; } = GlobalDid; // new Did("mstd:dir").ToString();

        public async Task<IEnumerable<FileInformation>> GetAllFiles()
        {
            return await GetFiles(new List<string> { "Rule", "Content", "Layer", "Routing" });
        }

        public async Task<string> GetFileContent(string contentId)
        {
            var contentGrain = await OrleansConnectionProvider.Client.GetGrain<IContentPersistentGrain>(contentId).Load();
            return contentGrain?.ContentAs<string>() ?? string.Empty;
        }

        private async Task<IEnumerable<FileInformation>> GetFiles(IEnumerable<string> directories)
        {
            var result = new List<FileInformation>();
            var directoryGrain = OrleansConnectionProvider.Client.GetGrain<IDirectoryGrain>(Did);
            foreach (var directory in directories)
            {
                if (!await directoryGrain.DirectoryExists(directory))
                {
                    continue;
                }
                var dir = await directoryGrain.GetDirectory(directory);
                var contentsGrain = OrleansConnectionProvider.Client.GetGrain<IDirectoryContentsGrain>(dir.ItemsGrainId);
                var contents = await contentsGrain.ListItems();
                foreach (var item in contents.Items)
                {
                    var fileName = item.Value.MetaData;
                    var contentGrain = await OrleansConnectionProvider.Client.GetGrain<IContentPersistentGrain>(item.Value.GrainId).Load();
                    var content = contentGrain.ContentAs<string>();
                    result.Add(new FileInformation
                    {
                        Id = item.Value.GrainId,
                        Directory = directory,
                        FileName = fileName
                    });
                }
            }
            return result;
        }

        public async Task<string> WriteFile(string directoryName, string fileName, string content, string contentId = null)
        {
            //directory
            var directoryGrain = OrleansConnectionProvider.Client.GetGrain<IDirectoryGrain>(Did);
            if (!await directoryGrain.DirectoryExists(directoryName))
            {
                await directoryGrain.CreateDirectory(directoryName);
            }
            var dir = await directoryGrain.GetDirectory(directoryName);
            //content
            var addItem = false;
            if (contentId == null)
            {
                addItem = true;
                contentId = new Did("mstd:pub").ToString();
            }
            var contentGrain = OrleansConnectionProvider.Client.GetGrain<IContentPersistentGrain>(contentId);
            await contentGrain.Save("text/yaml", EncodingType.UTF8, content);
            //add the content to the directory
            if (addItem)
            {
                var directoryContentsGrain = OrleansConnectionProvider.Client.GetGrain<IDirectoryContentsGrain>(dir.ItemsGrainId);
                await directoryContentsGrain.AddItem(new DirectoryContentsItem()
                {
                    MetaData = fileName,
                    GrainId = contentId,
                    Interface = typeof(IContentPersistentGrain)
                });
            }

            return contentId;
        }
    }
}
