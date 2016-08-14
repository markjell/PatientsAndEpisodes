using RestApi.Models;
using System.Data.Entity;

namespace RestApi.Tests
{
    public class TestPatientContext : IPatientContext
    {
        public TestPatientContext()
        {
            this.Patients = new TestDbSet<Patient>();
            this.Episodes = new TestDbSet<Episode>();
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Episode> Episodes { get; set; }
    }
}
