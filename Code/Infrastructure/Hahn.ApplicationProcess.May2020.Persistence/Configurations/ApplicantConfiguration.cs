using Hahn.ApplicationProcess.May2020.Domain.Entities.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hahn.ApplicationProcess.May2020.Persistence.Configurations
{
    public class ApplicantConfiguration : IEntityTypeConfiguration<Applicant>
    {
        public void Configure(EntityTypeBuilder<Applicant> builder)
        {
            builder.HasKey(e => e.ApplicantId);
            builder.Property(e => e.ApplicantId).UseIdentityColumn();
            builder.Property(e => e.Address).IsRequired().HasMaxLength(500);
            builder.Property(e => e.CountryOfOrigin).IsRequired().HasMaxLength(200);
            builder.Property(e => e.EmailAddress).IsRequired().HasMaxLength(200);
            builder.Property(e => e.FamilyName).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
            builder.ToTable("Applicant");
        }
    }
}
