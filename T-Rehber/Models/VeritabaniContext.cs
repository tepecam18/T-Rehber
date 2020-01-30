using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace T_Rehber.Models
{
    public class VeritabaniContext : DbContext
    {
        public VeritabaniContext(): base("Entities")
        {
            Database.SetInitializer<VeritabaniContext>
                (new DropCreateDatabaseIfModelChanges<VeritabaniContext>());
        }
        public DbSet<Calisan> Calisanlar { get; set; }
        public DbSet<Departman> Departmanlar { get; set; }
    }
}