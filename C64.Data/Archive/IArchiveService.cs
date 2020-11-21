namespace C64.Data.Archive
{
    public interface IArchiveService
    {
        void Load(byte[] archiveData);

        ArchiveInfo ArchiveInfo { get; }

        byte[] GetFile(string FileName);

        byte[] AddFileId();
    }

    public interface IFallbackArchiveService : IArchiveService
    { }
}