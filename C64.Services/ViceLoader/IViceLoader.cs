namespace C64.Services.ViceLoader
{
    public interface IViceLoader
    {
        (string SetupEmu, object SetupEmuParameters) ProcessFile(int productionFileId, string filename, byte[] fileData);
    }
}