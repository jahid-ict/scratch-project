namespace ScratchProject.Api.Intefaces
{
    public interface IMongoDbClientService
    {
        public object Get(string title = "");

        public void Save<T>(T value);

        public void SaveMany<T>(List<T> values);

        public T GetItemByField<T>(string fieldName, string value);

        public List<T> GetItemsByField<T>(string fieldName, string value);
    }
}
