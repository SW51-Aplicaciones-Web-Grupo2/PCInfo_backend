namespace PCInfo_backend.PCInfo.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}