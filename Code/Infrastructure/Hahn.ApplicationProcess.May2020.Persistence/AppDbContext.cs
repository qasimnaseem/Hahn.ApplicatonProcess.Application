using Hahn.ApplicationProcess.May2020.Application.Common.Interfaces;
using Hahn.ApplicationProcess.May2020.Domain.Entities.CustomEntities;
using Hahn.ApplicationProcess.May2020.Domain.Entities.DbEntities;
using Hahn.ApplicationProcess.May2020.Domain.Interfaces;
using Hahn.ApplicationProcess.May2020.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.May2020.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        private readonly IDateTime _dateTime;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            
            IDateTime dateTime)
            : base(options)
        {
            _dateTime = dateTime;
        }

        public override ChangeTracker ChangeTracker => base.ChangeTracker;

        public DbSet<Applicant> Applicants { get; set; }
        
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        Entry(entry.Entity).Property(x => x.CreatedOn).IsModified = false;
                        entry.Entity.ModifiedOn = _dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ApplicantConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public override EntityEntry Entry(object entity)
        {
            return base.Entry(entity);
        }

        public override EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Entry(entity);
        }
    }
}
