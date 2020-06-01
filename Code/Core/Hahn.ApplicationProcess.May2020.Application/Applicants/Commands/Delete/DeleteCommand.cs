using Hahn.ApplicationProcess.May2020.Application.Common.Exceptions;
using Hahn.ApplicationProcess.May2020.Application.Common.Interfaces;
using Hahn.ApplicationProcess.May2020.Domain.Entities.DbEntities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.May2020.Application.Applicants.Commands.Delete
{
    public class DeleteCommand : IRequest<bool>
    {
        public DeleteCommand() { }
        
        public DeleteCommand(int applicantId)
        {
            ApplicantId = applicantId;
        }
        
        public int ApplicantId { get; set; }

        public class Handler : IRequestHandler<DeleteCommand, bool>
        {
            private readonly IAppDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IAppDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<bool> Handle(DeleteCommand request, CancellationToken cancellationToken)
            {
                var entity = new Applicant { ApplicantId = request.ApplicantId, IsDeleted = true };
                _context.Applicants.Attach(entity);
                _context.Entry(entity).Property(x => x.IsDeleted).IsModified = true;

                try
                {
                    await _context.SaveChangesAsync(cancellationToken);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw new NotFoundException(nameof(Applicant), request.ApplicantId);
                }

                return true;
            }
        }
    }
}
