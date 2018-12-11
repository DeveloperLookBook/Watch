using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Watch.Models;
using Watch.Models.Users;
using Watch.Models.Watches;
using Xamarin.Forms;

namespace Watch.Databases
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        private string                      SqlLiteDbName  { get;      }
        private string                      PathToDatabase { get;      }
        public  DbSet<User>                 Users          { get; set; }
        public  DbSet<Models.Watches.Watch> Watch          { get; set; }


        public AppDbContext()
        {
            ISqlLite configs = DependencyService.Get<ISqlLite>();

            this.SqlLiteDbName  = "WatchApp.db"; ;
            this.PathToDatabase = configs.GetDatabasePath(this.SqlLiteDbName);
        }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={this.PathToDatabase}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            this.ApplyModelConfiguration(modelBuilder);
        }

        private void ApplyModelConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WatchModelConfiguration())
                        .ApplyConfiguration(new UserModelConfiguration ());
        }
    }
}
