using System.Threading.Tasks;

namespace Test.DAL.Abstract
{
    public interface IUnitOfWork
    {
        IProductsRepository ProductsRepository { get; }
        Task<int> Save();
    }
}
