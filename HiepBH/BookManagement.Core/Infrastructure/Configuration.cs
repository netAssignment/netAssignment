using BookManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace BookManagement.Core.Infrastructure
{
    public class Configuration : DbMigrationsConfiguration<BookManagementDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BookManagementDb context)
        {
            context.Accounts.AddOrUpdate(m => m.UserName,
                new Account { UserName = "hiep", Password = "hiep" });

            base.Seed(context);
        }
    }
}
