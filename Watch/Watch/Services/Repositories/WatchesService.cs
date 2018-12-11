using System;
using System.Collections.Generic;
using System.Text;
using Watch.Databases;

namespace Watch.Services.Repositories
{
    public class WatchesService : RepositoryService<Models.Watches.Watch>, IWatchesService
    {
        public WatchesService(IAppDbContext context) : base(context)
        {
        }
    }
}
