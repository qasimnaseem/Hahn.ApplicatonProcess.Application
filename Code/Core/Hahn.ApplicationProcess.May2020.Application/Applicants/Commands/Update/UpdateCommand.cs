using AutoMapper;
using Hahn.ApplicationProcess.May2020.Application.Common.Exceptions;
using Hahn.ApplicationProcess.May2020.Application.Common.Interfaces;
using Hahn.ApplicationProcess.May2020.Domain.Entities.DbEntities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.May2020.Application.Applicants.Commands.Update
{
    public class UpdateCommand :  IRequest<Applicant>, IMapFrom<Applicant>
    {
        public int ApplicantId { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public string CountryOfOrigin { get; set; }
        public string EmailAddress { get; set; }
        public int Age { get; set; }
        public bool Hired { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCommand, Applicant>();
        }

        public class Handler : IRequestHandler<UpdateCommand, Applicant>
        {
            private readonly IAppDbContext _context;
            private readonly IMediator _mediator;
            private readonly IMapper _mapper;

            public Handler(IAppDbContext context, IMediator mediator, IMapper mapper)
            {
                _context = context;
                _mediator = mediator;
                _mapper = mapper;
            }

            public async Task<Applicant> Handle(UpdateCommand request, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<Applicant>(request);
                _context.Applicants.Update(entity);

                try
                {
                    await _context.SaveChangesAsync(cancellationToken);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw new NotFoundException(nameof(Applicant), request.ApplicantId);
                }
                
                return entity;
            }
        }
    }
}
