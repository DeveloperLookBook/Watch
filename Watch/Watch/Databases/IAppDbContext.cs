using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;
using Watch.Models.Users;
using Watch.Models.Watches;

namespace Watch.Databases
{
    public interface IAppDbContext
    {
        DbSet<User>                 Users { get; set; }
        DbSet<Models.Watches.Watch> Watch { get; set; }

        DbSet<TModel>       Set<TModel>     () where TModel : class;
        EntityEntry<TModel> Add<TModel>     (TModel model) where TModel : class;
        int                 SaveChanges     ();
        Task<int>           SaveChangesAsync(CancellationToken token = default(CancellationToken));
        void                Dispose         ();
    }
}