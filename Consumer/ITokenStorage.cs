namespace Consumer
{
    public interface ITokenStorage
    {
        string Get();
        void Set(string value);
    }
}