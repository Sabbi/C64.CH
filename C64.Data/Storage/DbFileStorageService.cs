using C64.Data.Entities;
using C64.Services.Archive;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace C64.Data.Storage
{
    public class DbFileStorageService : IFileStorageService
    {
        private readonly ApplicationDbContext context;
        private readonly IArchiveService archiveService;

        public DbFileStorageService(ApplicationDbContext context, IArchiveService archiveService)
        {
            this.context = context;
            this.archiveService = archiveService;
        }

        public async Task DeleteFile(string container, string fileName)
        {
            var fileToDelete = context.DbFiles.FirstOrDefault(p => p.Container == container && p.FileName == fileName);
            context.Remove(fileToDelete);
            await context.SaveChangesAsync();
        }

        public async Task<byte[]> GetFileContents(string container, string fileName)
        {
            var fileToGet = await context.DbFiles.FirstOrDefaultAsync(p => p.Container == container && p.FileName == fileName);
            if (fileToGet != null)
                return fileToGet.Data;

            throw new FileNotFoundException("Cannot find file", container + "/" + fileName);
        }

        public async Task<FileInformation> GetFileInformations(string container, string fileName)
        {
            var fileToGet = await context.DbFiles.FirstOrDefaultAsync(p => p.Container == container && p.FileName == fileName);
            if (fileToGet != null)
            {
                return new FileInformation
                {
                    Created = fileToGet.CreatedAt,
                    FileSize = fileToGet.Size
                };
            }

            throw new FileNotFoundException("Cannot find file", container + "/" + fileName);
        }

        public async Task ReplaceFile(byte[] content, string container, string fileName)
        {
            var fileToUpdate = await context.DbFiles.FirstOrDefaultAsync(p => p.Container == container && p.FileName == fileName);

            if (fileToUpdate == null)
                throw new NotImplementedException();

            fileToUpdate.Data = content;
            fileToUpdate.CreatedAt = DateTime.Now;

            await context.SaveChangesAsync();
        }

        public async Task<string> SaveFile(byte[] content, string container, string fileName)
        {
            if (Path.GetExtension(fileName).ToLower() == ".zip")
            {
                archiveService.Load(content);
                content = archiveService.AddFileId();
            }

            var freeFileFound = false;

            var newFileName = fileName;

            var nextFileNumberCounter = 0;

            while (!freeFileFound)
            {
                if (!FileExists(container, newFileName))
                    freeFileFound = true;
                else
                {
                    // Add a counter if file is already taken.
                    nextFileNumberCounter++;
                    var rnd = nextFileNumberCounter.ToString("D3");

                    newFileName = Path.GetFileNameWithoutExtension(fileName) + "-" + rnd + Path.GetExtension(fileName);
                }
            }

            var file = new DbFile
            {
                Data = content,
                Container = container,
                CreatedAt = DateTime.Now,
                FileName = newFileName,
                Size = content.Length
            };

            context.DbFiles.Add(file);
            await context.SaveChangesAsync();
            return newFileName;
        }

        private bool FileExists(string container, string fileName)
        {
            return context.DbFiles.Any(p => p.Container == container && p.FileName == fileName);
        }
    }
}