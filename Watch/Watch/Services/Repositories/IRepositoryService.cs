using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Watch.Services.Repositories
{
    public interface IRepositoryService<TModel> where TModel : class
    {
        Task<T> ReadAsync<T>(Func<IQueryable<TModel>, T> builder, CancellationToken token = default(CancellationToken));
        Task    AddAsync    (TModel model, CancellationToken token = default(CancellationToken));
        Task    RemoveAsync (TModel model, CancellationToken token = default(CancellationToken));
        Task    UpdateAsync (TModel model, CancellationToken token = default(CancellationToken));

        void    SaveChangesAsync();
    }
}