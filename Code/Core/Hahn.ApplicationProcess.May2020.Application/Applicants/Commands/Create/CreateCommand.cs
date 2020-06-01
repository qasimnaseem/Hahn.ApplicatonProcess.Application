using AutoMapper;
using Hahn.ApplicationProcess.May2020.Application.Common.Interfaces;
using Hahn.ApplicationProcess.May2020.Domain.Entities.DbEntities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.May2020.Application.Applicants.Commands.Create
{
    public class CreateCommand : IRequest<Applicant>, IMapFrom<Applicant>
    {
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public string CountryOfOrigin { get; set; }
        public string EmailAddress { get; set; }
        public int Age { get; set; }
        public bool Hired { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCommand, Applicant>();
        }

        public class Handler : IRequestHandler<CreateCommand, Applicant>
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

            public async Task<Applicant> Handle(CreateCommand request, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<Applicant>(request);
                _context.Applicants.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return entity;
            }
        }
    }
}
