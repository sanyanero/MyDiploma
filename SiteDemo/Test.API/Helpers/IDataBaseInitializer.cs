using System.Threading.Tasks;

namespace Test.API.Helpers
{
    public interface IDataBaseInitializer
    {
        Task Initialize();
    }
}