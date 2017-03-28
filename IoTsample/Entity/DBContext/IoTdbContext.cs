namespace Entity.DBContext
{
    using Entity.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class IoTdbContext : DbContext
    {
        public IoTdbContext()
            : base("name=IoTDB")
        {
            Database.SetInitializer(new Configuration());
        }
        public virtual DbSet<Switch> Switch { get; set; }

        public class Configuration : CreateDatabaseIfNotExists<IoTdbContext>
        {
            protected override void Seed(IoTdbContext context)
            {
                context.Switch.Add(new Switch() { IsOpen = true, Date = DateTime.Now });

                context.SaveChanges();
            }
        }
    }
}