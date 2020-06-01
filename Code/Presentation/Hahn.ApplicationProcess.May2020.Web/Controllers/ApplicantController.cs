using Hahn.ApplicationProcess.May2020.Application.Applicants.Commands.Create;
using Hahn.ApplicationProcess.May2020.Application.Applicants.Commands.Delete;
using Hahn.ApplicationProcess.May2020.Application.Applicants.Commands.Update;
using Hahn.ApplicationProcess.May2020.Application.Applicants.Queries.GetAll;
using Hahn.ApplicationProcess.May2020.Application.Applicants.Queries.GetById;
using Hahn.ApplicationProcess.May2020.Domain.Entities.DbEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.May2020.Web.Controllers
{
    public class ApplicantController : BaseController
    {
        [HttpGet("{applicantId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Applicant>> GetById(int applicantId)
        {
            return Ok(await Mediator.Send(new GetByIdQuery(applicantId)));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Applicant>> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllQuery()));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Insert(CreateCommand command)
        {
            var created = await Mediator.Send(command);
            var url = $"{UriHelper.GetDisplayUrl(Request)}/{created.ApplicantId}";
            return Created(url, created);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update(UpdateCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{applicantId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int applicantId)
        {
            await Mediator.Send(new DeleteCommand(applicantId));
            return NoContent();
        }
    }
}