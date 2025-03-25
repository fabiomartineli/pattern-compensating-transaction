using Domain.Abstractions.Repositories;
using Infra.Data.Base;

namespace Infra.Data.Repositories
{
    public interface IBaseRepositoryInjector
    {
        IDatabaseContext Context { get; }
        IUnitOfWork UnitOfWork { get; }
    }
}
