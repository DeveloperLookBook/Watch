using System;
using System.Collections.Generic;
using System.Text;

namespace Watch.Models
{
    public abstract class Model<TId> : IModel<TId>
    {
        #region FIELDS

        private TId      _id;
        private DateTime _created = DateTime.Now;

        #endregion


        #region PROPERTIES

        public TId      Id      { get => this._id; protected set => this._id = value; }
        public DateTime Created => this._created;

        #endregion
    }
}
