namespace ProjectToyStore.Data.UnitOfWork
{
    public interface IUnitOfWorks
    {
        ToyContext context { get; }

        void Commit();
        void Dispose();
        void Rowback();
    }
}