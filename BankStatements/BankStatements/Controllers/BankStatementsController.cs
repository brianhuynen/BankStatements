using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

using BankStatements.Exceptions;
using BankStatements.Models.Domain;
using BankStatements.Models.DTO.BankStatements;
using BankStatements.Services;

//using Microsoft.AspNetCore.Authorization;

namespace BankStatements.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class BankStatementsController : Controller
    {
        //init mapper for DTO operations
        private readonly IMapper _mapper;
        //Add the service to the controller through Dependency Injection
        private readonly IBankStatementService _bankStatementService;

        public BankStatementsController(IMapper mapper, IBankStatementService bankStatementService)
        {
            _mapper = mapper;
            _bankStatementService = bankStatementService;
        }

        /// <summary>
        /// Returns a list of all BankStatements in the DB
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankStatementReadDTO>>> GetBankStatements()
        {
            return _mapper.Map<List<BankStatementReadDTO>>(await _bankStatementService.GetAllStatementsAsync());
        }

        /// <summary>
        /// Adds a BankStatement through a ReadDTO to the DB
        /// </summary>
        /// <param name="dtoStatement"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        /*
         * //For Authorication purposes, we would need to add this attribute
         * [Authorize(Roles = "editor")]
         */
        public async Task<ActionResult> PostBankStatement(BankStatementCreateDTO dtoStatement)
        {
            BankStatement bankStatement = _mapper.Map<BankStatement>(dtoStatement);

            try
            {
                await _bankStatementService.AddStatementAsync(bankStatement);
                return CreatedAtAction(nameof(GetBankStatements),
                    new { id = bankStatement.Id },
                    _mapper.Map<BankStatementReadDTO>(bankStatement));
            }
            catch (Exception ex) 
            { 
                if(ex.GetType() == typeof(InvalidIbanException))
                    return UnprocessableEntity(ex.Message);
                if(ex.GetType() == typeof(DuplicateReferenceIdException))
                    return Conflict(ex.Message);
                if(ex.GetType() == typeof(MutationMismatchException))
                    return Conflict(ex.Message);
            }
            return NoContent();
        }
    }
}
