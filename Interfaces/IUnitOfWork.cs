namespace HSPAWebAPI.Interfaces
{
    public interface IUnitOfWork
    {

        ICityRepository CityRepository { get; }

        Task<bool> SaveAsync();
    }
}
