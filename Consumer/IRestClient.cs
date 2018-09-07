using System.Threading.Tasks;

namespace Consumer
{
    public interface IRestClient
    {
        Task<string> GetValues();
    }
}