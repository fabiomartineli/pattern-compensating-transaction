using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Abstractions.Repositories
{
    public interface IUnitOfWork
    {
        Task StartStranctionAsync(CancellationToken cancellationToken);
        Task CommitTranscationAsync(CancellationToken cancellationToken);
        IDisposable CurrentTranscation { get; }
    }
}
