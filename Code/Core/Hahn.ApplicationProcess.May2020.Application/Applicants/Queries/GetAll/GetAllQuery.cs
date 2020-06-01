using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hahn.ApplicationProcess.May2020.Application.Applicants.DataTransferObjects;
using Hahn.ApplicationProcess.May2020.Application.Common.Interfaces;
using Hahn.ApplicationProcess.May2020.Domain.Entities.DbEntities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.May2020.Application.Applicants.Queries.GetAll
{
    public class GetAllQuery : IRequest<ApplicantListDTO>
    {
        public class GetByIdQueryHandler : IRequestHandler<GetAllQuery, ApplicantListDTO>
        {
            private readonly IAppDbContext _context;
            private readonly IMapper _mapper;

            public GetByIdQueryHandler(IAppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ApplicantListDTO> Handle(GetAllQuery request, CancellationToken cancellationToken)
            {
                var applicants = await _context.Applicants.AsNoTracking().
                    Where(a => a.IsDeleted == false).
                    ProjectTo<ApplicantDTO>(_mapper.ConfigurationProvider).
                    ToListAsync(cancellationToken);

                return new ApplicantListDTO { Applicants = applicants };
            }
        }
    }
}
