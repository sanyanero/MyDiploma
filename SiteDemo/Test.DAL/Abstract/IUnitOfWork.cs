using System.Threading.Tasks;

namespace Test.DAL.Abstract
{
    public interface IUnitOfWork
    {
        IQuestionsRepository QuestionsRepository { get; }
        Task<int> Save();
    }
}
