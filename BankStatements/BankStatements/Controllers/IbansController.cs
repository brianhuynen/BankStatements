using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankStatements.Models;
using BankStatements.Models.Domain;
using System.Net.Mime;
using AutoMapper;
using BankStatements.Services;
using BankStatements.Models.DTO.Ibans;

namespace BankStatements.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class IbansController : ControllerBase
    {
        //init mapper for DTO operations
        private readonly IMapper _mapper;
        //Add the service to the controller through Dependency Injection
        private readonly IIbanService _ibanService;

        public IbansController(IMapper mapper, IIbanService ibanService)
        {
            _mapper = mapper;
            _ibanService = ibanService;
        }

        /// <summary>
        /// Gets all Iban from the DB
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IbanReadDTO>>> GetIbans()
        {
            return _mapper.Map<List<IbanReadDTO>>(await _ibanService.GetAllIbanAsync());
        }
    }
}
