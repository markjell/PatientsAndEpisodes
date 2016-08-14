using System.Data.Entity;

namespace RestApi.Models
{
    public interface IPatientContext
    {
        DbSet<Patient> Patients { get; set; }
        DbSet<Episode> Episodes { get; set; }
    }
}
