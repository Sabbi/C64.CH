namespace C64.Services.ViceLoader
{
    public interface IViceLoader
    {
        (string SetupEmu, object SetupEmuParameters, bool enableDiskChange) ProcessFile(int productionFileId, string filename, byte[] fileData);
    }
}