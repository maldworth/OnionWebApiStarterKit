using OnionWebApiStarterKit.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionWebApiStarterKit.Bootstrapper
{
    public class SchoolContextDevelopmentConfiguration : DbConfiguration
    {
        public SchoolContextDevelopmentConfiguration()
        {
            SetDatabaseInitializer<SchoolDbContext>(new SchoolDbInitializer());
            SetDefaultConnectionFactory(new LocalDbConnectionFactory("v11.0"));
        }
    }

    public class SchoolDbInitializer : DropCreateDatabaseAlways<SchoolDbContext>
    {
        protected override void Seed(SchoolDbContext context)
        {
            SchoolDbSeed.Seed(context);
            base.Seed(context);
        }
    }
}
