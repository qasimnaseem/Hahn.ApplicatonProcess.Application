using System;

namespace Hahn.ApplicationProcess.May2020.Domain.Entities.CustomEntities
{
    public class AuditableEntity
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
