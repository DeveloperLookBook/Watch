using System;
using System.Collections.Generic;
using System.Text;

namespace Watch.Models
{
    public interface IModel<TId>
    {
        TId      Id      { get; }
        DateTime Created { get; }
    }
}
