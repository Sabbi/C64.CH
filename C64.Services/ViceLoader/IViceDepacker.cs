namespace C64.Services.ViceLoader
{
    public interface IViceDepacker
    {
        (string SetupEmu, object SetupEmuParameters, bool enableDiskChange) ProcessFile();
    }
}