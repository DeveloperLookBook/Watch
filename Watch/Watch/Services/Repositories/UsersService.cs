using System;
using System.Collections.Generic;
using System.Text;
using Watch.Databases;
using Watch.Models.Users;

namespace Watch.Services.Repositories
{
    public class UsersService : RepositoryService<User>, IUsersService
    {
        public UsersService(IAppDbContext context) : base(context)
        {
        }
    }
}
