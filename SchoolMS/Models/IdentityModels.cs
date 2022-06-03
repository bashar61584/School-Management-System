using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SchoolMS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<Program> Programs { get; set; }

        public DbSet<ProgramSession> ProgramSessions { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<ClassModel> ClassModels { get; set; }

        public DbSet<ClassSection> ClassSections { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Designation> Designations { get; set; }

        public DbSet<Gendertbl> Gendertbls { get; set; }

        public DbSet<Staff> Staffs { get; set; }

        public DbSet<ClassSubject> ClassSubjects { get; set; }

        public System.Data.Entity.DbSet<SchoolMS.Models.Annual> Annuals { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<SchoolMS.Models.StudentPromote> StudentPromotes { get; set; }

        public DbSet<SchoolMS.Models.MonthsModel> MonthsModels { get; set; }

        public DbSet<SchoolMS.Models.SubmessionFee> SubmessionFees { get; set; }

        public DbSet<SchoolMS.Models.TimeTable> TimeTables { get; set; }

        public DbSet<SchoolMS.Models.ExamType> ExamTypes { get; set; }

        public System.Data.Entity.DbSet<SchoolMS.Models.Exame> Exames { get; set; }
    }
}