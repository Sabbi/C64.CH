using System.Threading.Tasks;

namespace C64.Services.Storage
{
    public interface IFileStorageService
    {
        Task ReplaceFile(byte[] content, string container, string fileName);

        Task DeleteFile(string container, string fileName);

        Task<string> SaveFile(byte[] content, string container, string fileName);

        Task<byte[]> GetFileContents(string container, string fileName);

        Task<FileInformation> GetFileInformations(string container, string fileName);
    }
}