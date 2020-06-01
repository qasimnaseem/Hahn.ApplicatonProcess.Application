using Hahn.ApplicationProcess.May2020.Domain.Entities.CustomEntities;

namespace Hahn.ApplicationProcess.May2020.Domain.Entities.DbEntities
{
    public class Applicant : AuditableEntity
    {
        public int ApplicantId { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public string CountryOfOrigin { get; set; }
        public string EmailAddress { get; set; }
        public int Age { get; set; }
        public bool Hired { get; set; }
    }
}