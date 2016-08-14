using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace RestApi.Models
{
    public class PatientContext : DbContext, IPatientContext
    {

        public PatientContext()
            : base("PatientContext")
        {
            Database.SetInitializer<PatientContext>(null);
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Episode> Episodes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}