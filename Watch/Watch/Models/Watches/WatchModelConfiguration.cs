using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Watch.Extensions;
using Xamarin.Forms;

namespace Watch.Models.Watches
{
    public class WatchModelConfiguration : ModelConfiguration<Watch, int>
    {
        public override void Configure(EntityTypeBuilder<Watch> builder)
        {
            base.Configure(builder);

            builder.Property(m => m.ArrowsColor).HasConversion<string>(
                v => v.ToHex(),
                v => Color.FromHex(v));

            builder.Property(m => m.DialColor  ).HasConversion<string>(
                v => v.ToHex(),
                v => Color.FromHex(v));

            builder.Property(m => m.TimeZone   ).HasConversion<string>(
                v => v.Id,
                v => TimeZoneInfo.FindSystemTimeZoneById(v));
            
            builder.Property(m => m.ArrowsColor).IsRequired();
            builder.Property(m => m.DialColor  ).IsRequired();
            builder.Property(m => m.TimeZone   ).IsRequired();
        }
    }
}
