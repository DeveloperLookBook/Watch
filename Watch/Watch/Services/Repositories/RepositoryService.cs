using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Watch.Databases;

namespace Watch.Services.Repositories
{
    public abstract class RepositoryService<TModel> : IRepositoryService<TModel>
        where TModel : class
    {
        #region PROPERTIES

        protected IAppDbContext Context { get; }
        protected DbSet<TModel> Models  { get; }

        #endregion


        #region CONSTRUCTORS

        public RepositoryService(IAppDbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            this.Models  = this.Context.Set<TModel>();
        }

        #endregion


        #region METHODS

        public Task<T>    ReadAsync<T>    (Func<IQueryable<TModel>, T> build, CancellationToken token = default(CancellationToken))
        {
            return Task.Run(() => build(this.Models), token);
        }
        public Task       AddAsync        (TModel model, CancellationToken token = default(CancellationToken))
        {
            return Task.Run(async () =>
            {
                await this.Models.AddAsync(model, token);
                await this.Context.SaveChangesAsync(token);
            }, token);
        }
        public Task       RemoveAsync     (TModel model, CancellationToken token = default(CancellationToken))
        {
            return Task.Run(async () =>
            {
                this.Models.Remove(model);
                await this.Context.SaveChangesAsync();
            }, token);
        }
        public Task       UpdateAsync     (TModel model, CancellationToken token = default(CancellationToken))
        {
            return Task.Run(async () =>
            {
                this.Models.Update(model);
                await this.Context.SaveChangesAsync(token);
            }, token);
        }
        public async void SaveChangesAsync()
        {
            await this.Context.SaveChangesAsync();
        }

        #endregion
    }
}
