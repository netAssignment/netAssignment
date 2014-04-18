using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using BookManagement.Core.Models;

namespace BookManagement.Core.Infrastructure
{
    public class BookManagementDb: DbContext, IBookManagementDataSource
    {
        private const string _defaultConnectionStringName = "DefaultConnection";

        public BookManagementDb()
            : base(_defaultConnectionStringName)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            MigrateDatabaseToLatestVersion<BookManagementDb, Configuration> migrateDatabaseConfiguration = new MigrateDatabaseToLatestVersion<BookManagementDb, Configuration>();
            Database.SetInitializer(migrateDatabaseConfiguration);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }


        IQueryable<Account> IBookManagementDataSource.Accounts
        {
            get { return Accounts; }
        }

        IQueryable<Book> IBookManagementDataSource.Books
        {
            get { return Books; }
        }

        IQueryable<Category> IBookManagementDataSource.Categories
        {
            get { return Categories; }
        }

        IQueryable<Comment> IBookManagementDataSource.Comments
        {
            get { return Comments; }
        }
    }
}
