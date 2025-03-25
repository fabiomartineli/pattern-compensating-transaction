using Domain.Abstractions.Repositories;
using Infra.Data.Base;

namespace Infra.Data.Repositories
{
    public class BaseRepositoryInjector : IBaseRepositoryInjector
    {
        private readonly IDatabaseContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public BaseRepositoryInjector(IDatabaseContext context,
            IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        IDatabaseContext IBaseRepositoryInjector.Context => _context;
        IUnitOfWork IBaseRepositoryInjector.UnitOfWork => _unitOfWork;
    }
}
