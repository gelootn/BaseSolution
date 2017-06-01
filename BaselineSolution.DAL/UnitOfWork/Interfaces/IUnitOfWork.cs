namespace BaselineSolution.DAL.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        void Commit(int userId);
    }
}
