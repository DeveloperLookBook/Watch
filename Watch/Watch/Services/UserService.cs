using System;
using System.Collections.Generic;
using System.Text;
using Watch.Models.Users;
using Watch.Services.Repositories;

namespace Watch.Services
{
    public class UserService : IUserService
    {
        public Guid Id { get; set; }
    }
}
