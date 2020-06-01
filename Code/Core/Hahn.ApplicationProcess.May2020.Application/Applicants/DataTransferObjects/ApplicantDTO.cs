using AutoMapper;
using Hahn.ApplicationProcess.May2020.Application.Common.Interfaces;
using Hahn.ApplicationProcess.May2020.Domain.Entities.DbEntities;
using System;

namespace Hahn.ApplicationProcess.May2020.Application.Applicants.DataTransferObjects
{
    public class ApplicantDTO : IMapFrom<Applicant>
    {
        public int ApplicantId { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public string CountryOfOrigin { get; set; }
        public string EmailAddress { get; set; }
        public int Age { get; set; }
        public bool Hired { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Applicant, ApplicantDTO>();
        }

    }
}
