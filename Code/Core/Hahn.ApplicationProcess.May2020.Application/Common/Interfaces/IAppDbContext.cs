using Hahn.ApplicationProcess.May2020.Domain.Entities.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.May2020.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        ChangeTracker ChangeTracker { get; }
        DbSet<Applicant> Applicants { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        EntityEntry Entry(object entity);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
