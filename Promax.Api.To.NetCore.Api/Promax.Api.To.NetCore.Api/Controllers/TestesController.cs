using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Promax.NetCore.Application.Dto;
using Promax.NetCore.Application.Services.Contracts;

namespace Promax.Api.To.NetCore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestesController : ControllerBase
    {
        private readonly ITesteApplicationService _testeApplicationService;

        public TestesController(ITesteApplicationService testeApplicationService)
        {
            _testeApplicationService = testeApplicationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TesteListagemDto>>> GetAtivosAsync()
        {
            return Ok(await _testeApplicationService.GetAtivosAsync());
        }
    }
}
