namespace ScratchProject.Api.Intefaces
{
    public interface IRedisService
    {
        public void SaveString(string key, string value);

        public void SaveHash(string key, object value);
        public string GetString(string key);
    }
}
