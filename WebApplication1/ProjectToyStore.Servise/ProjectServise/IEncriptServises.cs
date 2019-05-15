namespace ProjectToyStore.Servise.ProjectServise
{
    public interface IEncriptServises
    {
        string DencryptData(string toDencrypted);
        string EncryptData(string toEncrypted);
    }
}