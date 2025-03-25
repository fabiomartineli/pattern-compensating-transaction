using Domain.Abstractions.Repositories;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infra.Data.Base
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IDatabaseContext _context;
        private IClientSession _session;

        public UnitOfWork(IDatabaseContext context)
        {
            _context = context;
        }

        public IDisposable CurrentTranscation => _session;

        public async Task StartStranctionAsync(CancellationToken cancellationToken)
        {
            if (_session != null)
            {
                return;
            }

            var readConcern = new ReadConcern(ReadConcernLevel.Available);

            _session = await _context.Client.StartSessionAsync(new ClientSessionOptions
            {
                CausalConsistency = false,
                DefaultTransactionOptions = new TransactionOptions(readConcern: readConcern, maxCommitTime: TimeSpan.FromMinutes(1))
            }, cancellationToken);

            //_session.StartTransaction(); //DESABILITAR EM DESENVOLVIMENTO
        }

        public async Task CommitTranscationAsync(CancellationToken cancellationToken)
        {
            //_await _session.CommitTransactionAsync(cancellationToken); //DESABILITAR EM DESENVOLVIMENTO
            _session = null;
        }

        public void Dispose()
        {
            if (_session != null)
            {
                if (_session.IsInTransaction)
                {
                    _session.AbortTransaction();
                }
                _session.Dispose();
                _session = null;
            }

            GC.SuppressFinalize(this);
        }
    }
}
