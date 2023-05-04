using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BankAccount.Core.Data.DbContext
{
    public static class EnsureDatabase
    {
        public static void Created(SqliteConnection conn)
        {
            var builder = new DbContextOptionsBuilder<BankAccountDbContext>();
            builder.UseSqlite(conn);

            using var context = new BankAccountDbContext(builder.Options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
