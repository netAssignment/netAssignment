using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookManagement.Core.Models
{
    public interface IBookManagementDataSource
    {
        IQueryable<Account> Accounts { get; }

        IQueryable<Book> Books { get; }

        IQueryable<Category> Categories { get; }

        IQueryable<Comment> Comments { get; }
    }
}
