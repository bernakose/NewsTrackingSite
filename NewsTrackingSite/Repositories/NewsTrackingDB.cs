using NewsTrackingSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace NewsTrackingSite.Repositories
{
    public partial class NewsTrackingDB : DbContext
    {
        public NewsTrackingDB() : base("name=NewsTrackingDB")
        {
        }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Genre>()
                .HasMany(e => e.News)
                .WithMany(e => e.Genre)
                .Map(m => m.ToTable("News_Genre").MapLeftKey("GenreID").MapRightKey("NewsID"));

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }
    }
}