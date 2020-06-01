using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hahn.ApplicationProcess.May2020.Application.Applicants.DataTransferObjects;
using Hahn.ApplicationProcess.May2020.Application.Common.Exceptions;
using Hahn.ApplicationProcess.May2020.Application.Common.Interfaces;
using Hahn.ApplicationProcess.May2020.Domain.Entities.DbEntities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.May2020.Application.Applicants.Queries.GetById
{
    public class GetByIdQuery : IRequest<ApplicantDTO>
    {
        public GetByIdQuery() { }
        public GetByIdQuery(int applicantId)
        {
            ApplicantId = applicantId;
        }
        public int ApplicantId { get; set; }


        public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, ApplicantDTO>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public GetByIdQueryHandler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ApplicantDTO> Handle(GetByIdQuery request, CancellationToken cancellationToken)
            {
                var applicant = await _context.Applicants.AsNoTracking().
                    Where(a => a.ApplicantId == request.ApplicantId && a.IsDeleted == false).
                    ProjectTo<ApplicantDTO>(_mapper.ConfigurationProvider).
                    FirstOrDefaultAsync(cancellationToken);

                if (applicant == null)
                {
                    throw new NotFoundException(nameof(Applicant), request.ApplicantId);
                }

                return applicant;
            }
        }
    }
}
