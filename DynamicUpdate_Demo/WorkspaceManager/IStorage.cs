namespace WorkspaceManager
{
    internal interface IStorage
    {
        void Write(string key, string value);
        string Read(string key);
    }
}