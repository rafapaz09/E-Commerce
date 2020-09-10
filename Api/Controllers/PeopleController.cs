using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Models;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {

        private readonly IApiService ApiService;

        public PeopleController(IApiService _ApiService)
        {
            ApiService = _ApiService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>Get([FromRoute]int id)
        {
            try
            {
                var data = await ApiService.GetApiData<People>($"people/{id}/");
                return Ok(data);
            }
            catch(Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

    }
}
