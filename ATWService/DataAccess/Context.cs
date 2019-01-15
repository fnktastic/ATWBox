using ATWService.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATWService.DataAccess
{
    public class Context : DbContext
    {
        public Context() : base("atw") { }

        public DbSet<ReadType> Reads { get; set; }
        public DbSet<ReadingType> Readings { get; set; }
        public DbSet<ReaderType> Readers { get; set; }
    }

    public class Initializer : DropCreateDatabaseIfModelChanges<Context>
    {
        public Initializer()
        {
            using (var context = new Context())
            {
                InitializeDatabase(context);
            }
        }
    }
}
