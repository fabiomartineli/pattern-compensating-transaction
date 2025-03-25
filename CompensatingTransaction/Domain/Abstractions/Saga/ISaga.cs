using System.Threading;
using System.Threading.Tasks;

namespace Domain.Abstractions.Saga
{
    public interface ISaga<TRequest>
    {
        Task ExecuteTransactionAsync(TRequest request, CancellationToken cancellationToken);
        Task CompensateTransactionAsync(TRequest request, CancellationToken cancellationToken);
    }
}
