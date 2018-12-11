using System;
using System.Collections.Generic;

namespace Watch.Models.Users
{
    public interface IUser : IModel<Guid>
    {
        Credentials                Credentials { get; set; }
        List<Models.Watches.Watch> Watches     { get; set; }
    }
}