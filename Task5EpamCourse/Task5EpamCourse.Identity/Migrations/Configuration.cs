namespace Task5EpamCourse.Identity.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Task5EpamCourse.Identity.DbContext.AccountContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Task5EpamCourse.Identity.DbContext.AccountContext context)
        {
           
        }
    }
}
