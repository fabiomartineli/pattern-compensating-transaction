using Domain.Abstractions.Repositories;
using Domain.Entities;
using Infra.Data.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class BaseRepository<TEntity> where TEntity : Entity
    {
        private readonly IDatabaseContext _context;
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IMongoCollection<TEntity> _collection;

        public BaseRepository(IBaseRepositoryInjector injector, string collectionName)
        {
            _context = injector.Context;
            _unitOfWork = injector.UnitOfWork;
            _collection = _context.Database.GetCollection<TEntity>(collectionName);
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
           await _collection.InsertOneAsync(_unitOfWork.CurrentTranscation as IClientSessionHandle, entity, cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Where(x => x.Id == entity.Id);
            await _collection.ReplaceOneAsync(_unitOfWork.CurrentTranscation as IClientSessionHandle, filter, entity, cancellationToken: cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        protected async Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> query, CancellationToken cancellationToken)
        {
            return await _collection.Find(query).ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
