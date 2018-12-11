using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Watch.Models
{
    public class ModelConfiguration<TModel, TId> : IEntityTypeConfiguration<TModel>
        where TModel : class, IModel<TId>
        where TId    : IComparable<TId>
    {
        public virtual void Configure(EntityTypeBuilder<TModel> builder)
        {
            builder.Property(m => m.Id).HasField("_id");
            builder.HasKey  (m => m.Id);
            builder.Property(m => m.Created).HasField("_created");
        }
    }
}
